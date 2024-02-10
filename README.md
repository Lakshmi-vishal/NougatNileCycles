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
## Requirements fullfilled
In line with our objectives, we have met the following requirements:

REST API:  RESTful services are designed following best practices and are well-integrated with the frontend, supporting all required functionalities.
Swagger: Complete documentation of our APIs with Swagger is available, enhancing developer experience and ease of use.
![swagger](https://github.com/Lakshmi-vishal/ChocolateFactory/assets/84403688/a705a43f-bc63-43fd-bd90-61ad9c169778)

SQL Database: We utilize a structured SQL database for optimal data storage and retrieval, supporting the complex needs of our platform.
![data](https://github.com/Lakshmi-vishal/ChocolateFactory/assets/84403688/cb0ebcb0-7a66-472e-8a6a-e77045fea9b6)

Frontend Features: The frontend includes:
Advanced filters for searching and sorting data, allowing users to easily find the products or information they need.
Interactive tables for displaying data using material ui, providing a clear and organized view of product specifications, reviews, and more.
Additional visual components that enhance the overall user experience, including responsive layouts.
![nougrat](https://github.com/Lakshmi-vishal/ChocolateFactory/assets/84403688/c420c596-7a7c-4586-946f-03b919676ae7)
![products](https://github.![feature](https://github.com/Lakshmi-vishal/ChocolateFactory/assets/84403688/9dcf1ce8-c06f-4332-8d23-e3cfc3ef716c)
com/Lakshmi-vishal/ChocolateFactory/assets/84403688/06832d04-8d0a-4064-ab3a-9079323c9d97)


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
As the brand stride towards creating the Wide range of cycling products, our roadmap includes several ambitious enhancements:
In line with  commitment to early and frequent releases as I stick to agile manifesto, I'm excited to announce the availability of a preliminary version of Nougat Nile Cycles. This initial release includes foundational features that allow users to explore bicycle parts, read product reviews, and get a glimpse of our factory outlet.
 If I would develop this full stack project with some extra features, that I would aim to achieve in next release, I would add the following features in my application.
Virtual Reality Showroom
Introduce a VR showroom experience, enabling customers to virtually explore and interact with  bicycles and accessories in a 3D space from anywhere in the world.

Comprehensive Cycling Marketplace
Expand the platform to include a marketplace feature where third-party vendors can list their cycling-related products, creating a comprehensive catalog of cycling gear and accessories.

Community Features
Ride Sharing: Introduce a feature for cyclists to organize and join group rides, fostering community engagement.
Events and Workshops: Host virtual cycling events, workshops, and webinars with renowned cyclists and industry experts.
Cycling Challenges: Launch monthly cycling challenges with rewards to motivate cyclists to achieve their fitness goals.
Subscription Services
Offer premium subscription models providing exclusive benefits such as early access to new products, special discounts, and a premium customer support line.

