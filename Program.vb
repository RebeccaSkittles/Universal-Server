Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Xml
Imports System.Windows.Forms

Module Program
    Dim databaseFilePath As String = ""
    Dim currentBuildVersion As String = "03A23"

    Sub Main()
        Dim port As Integer = 8080
        Dim alternativePort As Integer = 80
        Dim ipAddress As IPAddress = GetLocalIPAddress()

        Console.WriteLine("Universal Server")
        Console.WriteLine("Version: 1.0.0.0 Build: {0}", currentBuildVersion)
        Console.WriteLine("(C) 2023 XR Development")
        Console.WriteLine()

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("[INFO]: Server Started on IP: {0} On Port: {1}", ipAddress, port)
        Console.ForegroundColor = ConsoleColor.White

        Console.WriteLine("[INFO]: Server Listening On Port: {0}. Waiting for incoming connections...", port)

        CheckInternetConnection()
        CheckDatabaseConnection()
        CheckForUpdates()

        Try
            ' Start the server logic here...
            Dim serverSocket As New TcpListener(ipAddress, port)
            serverSocket.Start()

            While True
                Dim clientSocket As TcpClient = serverSocket.AcceptTcpClient()

                Dim clientEndPoint As IPEndPoint = CType(clientSocket.Client.RemoteEndPoint, IPEndPoint)
                Dim clientIPAddress As IPAddress = clientEndPoint.Address

                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine("[INFO]: Client at {0} has connected to the server. Time: {1}", clientIPAddress, DateTime.Now.ToString())
                Console.ForegroundColor = ConsoleColor.White

                ' Handle client communication...
                Try
                    Dim reader As New StreamReader(clientSocket.GetStream())
                    Dim writer As New StreamWriter(clientSocket.GetStream())
                    writer.AutoFlush = True

                    Dim command As String = reader.ReadLine()

                    If command = "userver.request(code)" Then
                        Dim generatedCode As Integer = GenerateRandomCode()
                        writer.WriteLine(generatedCode)
                        Console.ForegroundColor = ConsoleColor.Green
                        Console.WriteLine("[INFO]: Client at {0} has requested a random code.", clientIPAddress)
                        Console.ForegroundColor = ConsoleColor.White
                        Console.WriteLine("[INFO]: Code has been sent to client at {0}", clientIPAddress)
                    Else
                        Console.ForegroundColor = ConsoleColor.Yellow
                        Console.WriteLine("[WARNING]: Invalid command received from client at {0}", clientIPAddress)
                        Console.ForegroundColor = ConsoleColor.White
                    End If

                Catch ex As IOException
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine("[WARNING]: An IO exception occurred while handling client at {0}. Reason: {1}", clientIPAddress, ex.Message)
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine("[WARNING]: Client at {0} has been kicked from the server. Reason: {1}", clientIPAddress, ex.Message)
                    Console.ForegroundColor = ConsoleColor.White

                Catch ex As Exception
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("[ERROR]: Unknown error occurred. Reason: {0}", ex.Message)
                    Console.ForegroundColor = ConsoleColor.White

                    ShowErrorMessageBox(ex.Message)
                Finally
                    clientSocket.Close()

                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("[INFO]: Client at {0} has disconnected from the server. Time: {1}", clientIPAddress, DateTime.Now.ToString())
                    Console.ForegroundColor = ConsoleColor.White
                End Try

            End While

            serverSocket.Stop()

        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("[ERROR]: Server failed to start on IP: {0} On Port: {1}. Reason: {2}", ipAddress, port, ex.Message)
            Console.ForegroundColor = ConsoleColor.White

            ShowErrorMessageBox(ex.Message)
        End Try

        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
    End Sub

    Function GetLocalIPAddress() As IPAddress
        Dim host As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())
        Dim ipAddress As IPAddress = host.AddressList.FirstOrDefault(Function(ip) ip.AddressFamily = AddressFamily.InterNetwork AndAlso Not IPAddress.IsLoopback(ip))

        Return ipAddress
    End Function

    Sub CheckInternetConnection()
        Try
            Using client As New WebClient()
                Using stream As Stream = client.OpenRead("http://www.google.com")
                    ' Server is connected to the internet!
                End Using
            End Using
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine("[WARNING]: The server is not connected to the internet. The server will only run locally on the network.")
            Console.ForegroundColor = ConsoleColor.White
        End Try
    End Sub

    Sub CheckDatabaseConnection()
        ' Check if the database file exists in the same directory as the server app
        databaseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "database.xml")
        If File.Exists(databaseFilePath) Then
            Try
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(databaseFilePath)

                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine("[INFO]: Database is connected to the server.")
                Console.ForegroundColor = ConsoleColor.White
            Catch ex As XmlException
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine("[WARNING]: The database file does not have the expected XML structure.")
                Console.ForegroundColor = ConsoleColor.White
            End Try
        Else
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine("[WARNING]: Server does not have a database connected. Clients that require a database won't be able to function properly.")
            Console.ForegroundColor = ConsoleColor.White
        End If
    End Sub

    Sub CheckForUpdates()
        Dim githubUrl As String = "https://github.com/RebeccaSkittles/Universal-Server"
        Dim latestBuildUrl As String = "https://github.com/RebeccaSkittles/Universal-Server/releases/latest"
        Dim latestBuildVersion As String = ""

        Try
            Dim request As HttpWebRequest = CType(WebRequest.Create(latestBuildUrl), HttpWebRequest)
            request.AllowAutoRedirect = False

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            latestBuildVersion = response.Headers("Location").Split("/"c).Last()
            response.Close()

            If latestBuildVersion <> currentBuildVersion Then
                ' Newer version available
                Console.ForegroundColor = ConsoleColor.Yellow
                Console.WriteLine("[WARNING]: There is a new build ({0}) available. Please download it from {1}.", latestBuildVersion, githubUrl)
                Console.ForegroundColor = ConsoleColor.White
            Else
                ' Server is already on the latest version
                Console.WriteLine("[INFO]: The server is running the latest version ({0}).", currentBuildVersion)
            End If

        Catch ex As Exception
            ' Error retrieving latest version
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("[ERROR]: An error occurred while checking for updates. Please visit {0} for the latest version. Reason: {1}", githubUrl, ex.Message)
            Console.ForegroundColor = ConsoleColor.White
        End Try
    End Sub

    Function GenerateRandomCode() As Integer
        Return New Random().Next(100000, 999999)
    End Function

    Sub ShowErrorMessageBox(crashReason As String)
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("[ERROR]: The server has unexpectedly crashed. Reason: {0}", crashReason)
        Console.ForegroundColor = ConsoleColor.White
    End Sub
End Module
