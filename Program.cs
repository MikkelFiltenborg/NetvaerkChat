using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
{

    public static void StartClient()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new byte[1024];

        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("192.168.1.2"), 9050);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);
                while (true)
                {
                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                    byte[] msg = Encoding.ASCII.GetBytes(Console.ReadLine());

                    int bytesSent = sender.Send(msg);

                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));




                }
                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }
}