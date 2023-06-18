# Universal Server

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

Universal Server is a versatile server application that allows you to handle incoming client connections and implement custom server logic. It provides a flexible framework for building server applications for various purposes.

## Features

- Easy-to-use server setup and configuration
- Support for handling incoming client connections
- Customizable server logic implementation
- Console interface for monitoring and managing the server
- Database integration for client data management (optional)
- Automatic update checks to keep the server up to date

## Installation


2. Build the project using your preferred IDE or the command line.
3. Run the compiled executable file.

## Getting Started

1. Clone the repository:

   ```shell
   git clone https://github.com/RebeccaSkittles/Universal-Server.git

2. Open within Visual Studio
3. than comile and build the server

### Prerequisites

- .NET Framework 4.7.2 or later

### Usage

1. Start the Universal Server application.

2. Configure the server settings, such as the port number and database connection details.

3. Click the "Start Server" button to begin listening for incoming connections.

4. Handle incoming client connections and implement your custom server logic.

5. Monitor client interactions and manage the server using the provided console interface.

### Configuration

#### !NOTE! This feature is still in development is curently univalibe.

To configure Universal Server, modify the settings in the `config.ini` file located in the project's root directory. The configuration file includes options for the server port number, database connection details, and other customizable parameters.

## Creating the Database

Universal Server requires a `database.xml` file to store and manage client and file information. Follow the steps below to create the `database.xml` file:

1. Navigate to the root directory of the Universal Server project.

2. Locate the `database.xml.template` file and make a copy of it.

3. Rename the copied file to `database.xml`.

4. Open the `database.xml` file using a text editor.

5. Customize the database structure and add any additional fields or elements as per your requirements.

6. Save the file.

The `database.xml` file will now serve as the database for the Universal Server, storing client information and file ownership details. Ensure that the server has read and write permissions to this file.

Note: If you already have an existing database file or want to use a different database format, make sure to update the `CheckDatabaseConnection()` function in the `Program.vb` file to handle the connection accordingly.

## Troubleshooting: Ensuring Port 8080 is Working

If you're having trouble accessing your application on port 8080, it's important to verify that the port is open and accessible. Follow these steps to troubleshoot the issue on a Windows system:

1. Check Listening Ports: Open a command prompt and execute the following command to view the list of listening ports on your system:

```netstat -a -n -o```

Look for an entry with "0.0.0.0:8080" under the "Local Address" column. If you don't see it, proceed to the next step.

2. Add Inbound Rule: Make sure the port is allowed in the Windows Firewall. Run the following command in an elevated command prompt to add an inbound rule for port 8080:


Look for an entry with "0.0.0.0:8080" under the "Local Address" column. If you don't see it, proceed to the next step.

2. Add Inbound Rule: Make sure the port is allowed in the Windows Firewall. Run the following command in an elevated command prompt to add an inbound rule for port 8080:

```netsh advfirewall firewall add rule name="Open Port 8080" dir=in action=allow protocol=TCP localport=8080```

If the command executes successfully, try accessing your application again on port 8080. If it still doesn't work, continue to the next step.

3. Disable Other Firewalls: Verify that there are no other firewall or security software blocking port 8080. Some antivirus or security software may have their own firewalls. Temporarily disable any other firewall or security software and test the application again. If it works after disabling the additional firewalls, you may need to configure them to allow port 8080.

4. Check for Port Conflicts: Ensure that no other application or service is already using port 8080. Use the following command to check for open ports and their associated processes:

```netstat -a -n -o```

Look for any entry with "0.0.0.0:8080" under the "Local Address" column. Note the corresponding PID (process ID) in the "PID" column. Use Task Manager (or other process management tools) to identify the process associated with that PID and decide how to handle it.

5. Restart Your Computer: Sometimes, changes made to the firewall settings or network configuration may require a system restart to take effect. Try restarting your computer and test the application again on port 8080.

If you've followed these troubleshooting steps and are still unable to get port 8080 to work, it's recommended to seek assistance from a technical expert who can help diagnose the issue further for your specific system configuration.

### Contributing

Contributions are welcome! To contribute to Universal Server, follow these steps:

1. Fork the project repository.

2. Create a new branch for your feature or bug fix.

3. Make the necessary changes and additions.

4. Test your changes thoroughly.

5. Commit and push your changes to your forked repository.

6. Submit a pull request to the main repository.

Please ensure your code follows the project's coding conventions and includes appropriate documentation.

## Known Supported Languages

- [VB.NET](https://www.visualstudio.com/)
- [Python](https://www.python.org/)

If you have successfully used Universal Server in another programming language, we would love to hear from you! Please [submit an issue](https://github.com/your/repository/issues) and let us know about your experience.

## Support

If you encounter any issues, have questions, or need assistance with Universal Server, please reach out to our support team.

- Email: support@xrdevelopment.net
- [Submit an issue](https://github.com/your/repository/issues)
- [Join Our Discord!](https://discord.gg/Edam9S2XAm)

Our support team is available to help you troubleshoot problems, provide guidance, and answer any questions you may have. Please provide detailed information about the issue you are experiencing to help us assist you more effectively.

We strive to provide timely responses and ensure a smooth experience with Universal Server.

Please note that support is provided on a best-effort basis, and response times may vary depending on the volume of inquiries.

For bug reports, feature requests, or general discussions, you can also create an issue on the project's GitHub repository.


### License

Universal Server is released under the MIT License. See the [LICENSE](LICENSE) file for more details.
