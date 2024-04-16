# program to send udp data to a remote computer
import socket
import random
import time

class UDPSocket:
    def __init__(self, ip: str, port: int):
        self.ip = ip
        self.port = port
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    def send(self, message: str):
        self.sock.sendto(message.encode(), (self.ip, self.port))

if __name__ == "__main__":
    server_address = '192.168.0.176' # ip addr to send to
    server_port = 5555 # port to send to

    # Create a UDP socket
    udp_socket = UDPSocket(server_address, server_port)

    while True:
        # send a random number between 0 and 100
        message = random.randint(0,100)
        # print("Sending: " + message)
        # udp_socket.send(f"{int(message/100 > 0.5)}, 0.5")
        udp_socket.send(f"{message}")
        time.sleep(1)


