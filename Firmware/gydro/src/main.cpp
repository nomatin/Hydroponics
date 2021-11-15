/*
  To upload through terminal you can use: curl -F "image=@firmware.bin" esp8266-webupdate.local/update
*/

#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266WebServer.h>
#include <ESP8266mDNS.h>
#include <ESP8266HTTPUpdateServer.h>
#include <WiFiUDP.h>
#define pinPump 5
#define pinSendor 4
bool wateringBool = true;
WiFiUDP Udp;
int DelayWoterPomp = 12000;
const char *host = "esp";
char incomingPacket[255];                             // buffer for incoming packets
char replyPacket[] = "Hi there! Got the message :-)"; // a reply string to send back
int delayWatering = 1.5 * 1000 * 60 * 60;
long int timerWatering = 0;
ESP8266WebServer httpServer(80);
ESP8266HTTPUpdateServer httpUpdater;
void watering()
{
  digitalWrite(pinPump, 1);
  delay(DelayWoterPomp);
  digitalWrite(pinPump, 0);
}
void parser()
{

  byte numOrder[10];
  int count = 0;
  char *pointer = incomingPacket;
  while (true)
  {
    numOrder[count++] = atoi(pointer);
    pointer = strchr(pointer, ':');

    if (pointer)
      pointer++;
    else
      break;
  }
  delayWatering = numOrder[0] * 1000 * 60 * 60;
  DelayWoterPomp = numOrder[1];
  Serial.println(numOrder[0]);
  Serial.println(numOrder[1]);
}
void setup(void)
{
  pinMode(pinPump, OUTPUT);
  pinMode(pinSendor, INPUT_PULLUP);
  WiFi.mode(WIFI_AP_STA);
  Serial.begin(115200);
  WiFi.begin("keenetic lite", "ananas20");
  WiFi.softAP("water", "12345678");

  Serial.print("Connecting"); //  "Подключение"
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println();

  Serial.print("Connected, IP address: ");
  //  "Подключились, IP-адрес: "
  Serial.println(WiFi.localIP());
  MDNS.begin(host);

  httpUpdater.setup(&httpServer);
  httpServer.begin();

  MDNS.addService("http", "tcp", 80);
  Serial.printf("HTTPUpdateServer ready! Open http://%s.local/update in your browser\n", host);
  Udp.beginMulticast(WiFi.localIP(), IPAddress(226, 1, 1, 1), 4096);
  Serial.println("\n connected udp multicast");
}

void loop(void)
{
  int packetSize = Udp.parsePacket();
  if (packetSize)
  {
    // receive incoming UDP packets
    Serial.printf("Received %d bytes from %s, port %d\n", packetSize, Udp.remoteIP().toString().c_str(), Udp.remotePort());
    int len = Udp.read(incomingPacket, 255);
    if (len > 0)
    {
      incomingPacket[len] = '\0';
    }
    Serial.println("UDP packet contents: %s\n");
    Serial.println(incomingPacket);
    parser();
  }
  if (millis() - timerWatering >= delayWatering || millis() <= DelayWoterPomp)
  {
    timerWatering = millis();
    watering();
    wateringBool = true;
  }
  /*
  if (wateringBool)
  {
    digitalWrite(pinPump, HIGH);
    if (!digitalRead(pinSendor))
    {
      wateringBool = false;
      digitalWrite(pinPump, LOW);
    }
  }
  */
  httpServer.handleClient();
  MDNS.update();
}