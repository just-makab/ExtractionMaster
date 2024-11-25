# EM WebApp
This project is a comprehensive web application for an extraction company, offering a seamless platform for managing services, bookings, and payments. Customers can book online for services, and notifications will be sent to both the company and the client to ensure smooth communication.

## Project Overview
The website provides the following features:

## Services Offered:
- **Installation:** Setting up new extraction systems.
- **Servicing:** Regular system checkups to ensure optimal performance.
- **Maintenance:** Repair and upkeep of extraction systems.

**Online Booking:**
- Customers can check available slots and book appointments online.
- Avoids double bookings by dynamically updating the calendar.

**Payment Integration:**
- Payments can be processed directly on the website using a secure portal.

**Notifications:**
- Both the company and the client receive confirmation notifications via email and WhatsApp for every booking or payment.

## Technology Stack
## Frontend:
- **Bootstrap:** For pre-designed buttons and responsive design.
- **Font Awesome:** For icons and fonts.
- **HTML:** For structuring content such as headings, paragraphs, buttons, and forms.
- **CSS:** For layouts, colors, fonts, and animations.

## Backend:
- **C#:** A versatile, object-oriented programming language for building robust and scalable applications.
- **.NET Core:** A cross-platform framework for building high-performance web and server applications.
- **Entity Framework Core:** Simplifies database interactions with object-relational mapping (ORM).
- **Twilio WhatsApp API:** Enables automated and programmatic WhatsApp messaging for seamless communication.
- **.NET.Mail:** A built-in library for sending and managing email notifications.

## Security Features
**Express-brute:**
- Protects against brute-force login attempts.
- Prevents Distributed Denial of Service (DDoS) attacks.

**Express-rate-limit:**
- Limits the number of requests a user can make in a specific timeframe to avoid overloading the system.
**Helmet:**
- Sets secure HTTP headers to prevent vulnerabilities like clickjacking, XSS, and session hijacking.

**jsonwebtoken (JWT):**
- Implements token-based authentication to protect against Man-in-the-Middle (MitM) attacks.

**HTTPS Encryption:**
- Ensures secure communication between the server and users.

**API Integrations:**
**Email API:**
- Used to send booking and payment confirmations to both the client and the company.

**WhatsApp API (Twilio):**
- Sends instant notifications for booking status, payment confirmations, and updates.

**Running Costs:**
- *Azure App Service*
  - **Cost:** R892.98/month
- *Azure SQL Database*
  - Low Traffic: R1,263.06/month
- *Best Case Scenario:**
  - R1,533.72/month
- *Worst Case Scenario:*
  - R6,676.18/month
- *Combined Monthly Costs*
  - Low Traffic: R2,155.37/month
- *Best Case:*
  - R2,697.35/month
- *Worst Case:*
  - R7,569.16/month

## How to Set Up the Project
**Clone the Repository:**
- bash
  git clone <repository-url>
cd <repository-folder>
**Install Dependencies:**
- bash
  dotnet restore
**Set Environment Variables:**
- Create a appsettings.json file with the following configuration:
    json
    {
        "JwtSecret": "your_jwt_secret",
        "Twilio": {
            "AccountSid": "your_twilio_account_sid",
            "AuthToken": "your_twilio_auth_token"
        },
        "Email": {
            "Service": "your_email_service",
            "User": "your_email_address",
            "Password": "your_email_password"
        }
    }
**Run the Application:**
- bash
  dotnet run
**Access the Application:**
- Visit http://localhost:5000 in your browser.
- API Endpoints
- Authentication
    *Register (POST /register)*
    *Request Body:*
    *    json*
    
   * {*
       * "name": "CustomerName",*
      *  "password": "SecurePassword",*
       * "email": "user@example.com"*
    *}*
    *Login (POST /login)*
    *Request Body:*
   * json*
   * {*
    *    "email": "user@example.com",*
     *   "password": "SecurePassword"*
    *}*
    **Booking:**
    *Book Appointment (POST /appointments)*
    *Request Body:*    
    *json*
    *{*
       *"serviceType": "Installation",*
         *"preferredDate": "YYYY-MM-DD",*
        *"customerId": "CustomerID12345"*
    *}*
**Check Availability (GET /appointments/available)**
**Query Parameter:**
- serviceType=Installation

## Conclusion
- This project is designed to provide a smooth, secure, and user-friendly experience for both the company and its clients. With advanced API integrations, robust security measures, and scalable architecture, it offers an efficient way to manage extraction services online.
