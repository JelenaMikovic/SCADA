# SCADA

- Overview:

This project focuses on implementing a Supervisory Control and Data Acquisition (SCADA) system, incorporating various functionalities to monitor and control processes. The system is designed to support analog and digital tags, user management, real-time data visualization, alarming, and reporting.

- Key Features:
1. Tag Management: Addition and removal of digital and analog tags with attributes like tag name, description, driver, I/O address, scan time, and more.
2. User Registration/Login: User authentication to access the Database Manager.
3. Real-Time Data Handling: Writing output tag values and displaying current values through the Database Manager.
4. Trending: Displaying real-time values of input tags through the Trending application.
5. Configuration File Handling: Reading and writing system configuration from/to the scadaConfig.xml file.

- Architecture:

1. Database Manager: Allows tag manipulation, user registration/login, and handling output tag values.
2. Trending App: Displays real-time values of input tags.
3. SCADA Core: The core implements server-client communication, housing the Simulation Driver and Tag Processing components. It handles server communication and tag value processing.

- Enhancements:

Real-Time Unit (RTU): Simulates field measurement devices, sending signed data to the service for validation before storage or transmission to other WCF clients.

Alarm Handling: Addition and removal of alarms for analog inputs, logging alarms to alarmsLog.txt, and displaying alarms in the Alarm Display client.

Persistence: Saving tag values in the database for persistence.

Report Manager: Generating various reports like all alarms in a specific time range, alarms of a specific priority, and last values of AI and DI tags.

- Conclusion:
  
This SCADA system project provides a solution for process monitoring and control. It includes features for user management, real-time data visualization, alarming, and reporting. The architecture ensures modularity and scalability, making it suitable for diverse industrial applications.
