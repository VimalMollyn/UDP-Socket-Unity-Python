import socket

# TCP server configuration
SERVER_IP = '127.0.0.1'  # IP address of the server
SERVER_PORT = 12345     # Port number of the server

# Create a TCP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to a specific IP address and port
sock.bind((SERVER_IP, SERVER_PORT))

# Listen for incoming connections
sock.listen(1)
print(f"TCP server is running on {SERVER_IP}:{SERVER_PORT}")

# Accept a client connection
client_sock, address = sock.accept()
print(f"Connected to client: {address}")

while True:
    # Receive data from the client
    data = client_sock.recv(1024)
    message = data.decode('utf-8')
    # print(f"Received message: {message}")

    # Break the loop if the received message is 'quit'
    if message.lower() == 'quit':
        break

    # Send a response back to the client
    response = f"Server received: {message}"
    client_sock.send(response.encode('utf-8'))

# Close the client socket
client_sock.close()

# Close the server socket
sock.close()
