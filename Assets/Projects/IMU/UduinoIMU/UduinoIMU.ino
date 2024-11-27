#include "Uduino.h"  // Include Uduino library at the top of the sketch
Uduino uduino("IMU");

#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#include "Wire.h"

MPU6050 mpu;

// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// orientation/motion vars
VectorInt16 aa;         // [x, y, z] accel sensor measurements

int16_t previousY = 0;  // Nilai Y sebelumnya
int16_t deltaY = 0;     // Delta perubahan Y

void setup() {
  Wire.begin();
  Wire.setClock(400000); // 400kHz I2C clock. Comment this line if having compilation difficulties
  Serial.begin(38400);

  while (!Serial); // wait for Leonardo enumeration, others continue immediately

  mpu.initialize();
  devStatus = mpu.dmpInitialize();

  mpu.setXGyroOffset(54); // Kalibrasi gyroscope
  mpu.setYGyroOffset(-21);
  mpu.setZGyroOffset(5);

  if (devStatus == 0) {
    mpu.setDMPEnabled(true);
    dmpReady = true;
    packetSize = mpu.dmpGetFIFOPacketSize();
  } else {
    Serial.println("Error initializing MPU6050!");
  }
}

void loop() {
  uduino.update();

  if (uduino.isInit()) {
    if (!dmpReady) {
      Serial.println("IMU not connected.");
      delay(10);
      return;
    }

    int mpuIntStatus = mpu.getIntStatus();
    fifoCount = mpu.getFIFOCount();

    if ((mpuIntStatus & 0x10) || fifoCount == 1024) { // check for overflow
      mpu.resetFIFO();
    } else if (mpuIntStatus & 0x02) {
      while (fifoCount < packetSize) fifoCount = mpu.getFIFOCount();
      mpu.getFIFOBytes(fifoBuffer, packetSize);
      fifoCount -= packetSize;

      CalculateDeltaY();  // Hitung delta Y
    }
  }
}

void CalculateDeltaY() {
  // Ambil data percepatan dari buffer
  mpu.dmpGetAccel(&aa, fifoBuffer);

  // Hitung delta perubahan pada sumbu Y
  deltaY = aa.y - previousY;

  if (deltaY > 0) {  // Jika delta lebih dari 0
    Serial.print("Delta Y : ");
    Serial.println(deltaY);
    TriggerPositiveDelta();  // Panggil fungsi jika delta lebih dari 0
  }

  // Update nilai Y sebelumnya
  previousY = aa.y;
}

void TriggerPositiveDelta() {
  // Fungsi yang dipanggil jika delta Y > 0
  Serial.println("Delta Y positif terdeteksi! Fungsi TriggerPositiveDelta dipanggil.");
}
