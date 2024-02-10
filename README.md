Nougat Nile Cycles
## About the project
Nougat Nile Cycles is an innovative platform designed for cycling enthusiasts, offering a comprehensive online experience for exploring, customizing, and purchasing high-quality bicycles. Our mission is to enhance the cycling experience for riders of all levels by providing
customizable options that meet individual preferences and performance needs. From detailed product reviews to a virtual showroom visit, Nougat Nile Cycles ensures every rider finds their perfect ride.
## Prerequisites

Before you begin, ensure you have met the following requirements:
- [.NET 5.0 SDK](https://dotnet.microsoft.com/download) or higher
- [Node.js](https://nodejs.org/en/) (LTS version recommended)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) (for Windows) or [Visual Studio Code](https://code.visualstudio.com/) (for Windows, Linux, and macOS) with the C# extension installed
- SQL Server (if your project uses a database)

## Getting Started


### Setup

1. **Clone the repository**

git clone https://github.com/Lakshmi-vishal/ChocolateFactory.git

2.  **Navigate to the project directory**


cd nougatnile-cycles

3.  **Install dependencies**



- dotnet add package Microsoft.EntityFrameworkCore.SqlServer

- dotnet add package Microsoft.EntityFrameworkCore.Design

-  dotnet ef dbcontext scaffold "Server=VH_LVH;Database=AdventureWorks2022;;Integrated Security=true;" 

-   Microsoft.EntityFrameworkCore.SqlServer -o Models

-  dotnet ef migrations add InitialCreate

-  dotnet ef database update --context AppDbContext

-   Update-Database -Context AppDbContext

-   dotnet ef migrations add InitialCreate --context AppDbContext

-  dotnet tool install --global dotnet-aspnet-codegenerator

-  dotnet add package Microsoft.EntityFrameworkCore.Tools

-  dotnet add package Swashbuckle.AspNetCore

-  npm install
(or) 
-  yarn install

3.  **Run the project**
Run the backend Asp.net using command
 dotnet run --urls "https://localhost:5201;http://localhost:5200"
Run the frontend React Development server using following command
cd factory 
npm start
or
yarn start
4.  **Features**
Customizable Bicycles
Product Reviews
Online Showroom
Order Viewing
Community Engagement
4.  **Future Enhancements**
As we stride towards creating the Wide range of cycling products, our roadmap includes several ambitious enhancements:

Virtual Reality Showroom
Introduce a VR showroom experience, enabling customers to virtually explore and interact with our bicycles and accessories in a 3D space from anywhere in the world.

Comprehensive Cycling Marketplace
Expand our platform to include a marketplace feature where third-party vendors can list their cycling-related products, creating a comprehensive catalog of cycling gear and accessories.

Community Features
Ride Sharing: Introduce a feature for cyclists to organize and join group rides, fostering community engagement.
Events and Workshops: Host virtual cycling events, workshops, and webinars with renowned cyclists and industry experts.
Cycling Challenges: Launch monthly cycling challenges with rewards to motivate cyclists to achieve their fitness goals.
Subscription Services
Offer premium subscription models providing exclusive benefits such as early access to new products, special discounts, and a premium customer support line.

Mobile Application
Further Development could be a Nougat Nile Cycles mobile app to provide a seamless shopping and community experience for cyclists on the go.