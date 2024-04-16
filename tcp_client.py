import socket

# TCP server configuration
SERVER_IP = '127.0.0.1'  # IP address of the server
SERVER_PORT = 12345     # Port number of the server

# Create a TCP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Connect to the server
sock.connect((SERVER_IP, SERVER_PORT))
print(f"Connected to server: {SERVER_IP}:{SERVER_PORT}")

while True:
    # Get the message to send from the user
    message = input("Enter the message to send (or 'quit' to exit): ")

    # Send the message to the server
    sock.send(message.encode('utf-8'))
    print(f"Sent message: {message}")

    # Break the loop if the user enters 'quit'
    if message.lower() == 'quit':
        break

    # Receive the response from the server
    data = sock.recv(1024)
    response = data.decode('utf-8')
    print(f"Received response: {response}")

# Close the socket
sock.close()
