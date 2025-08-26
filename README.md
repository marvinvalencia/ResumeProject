Hi, This is my personal learning project where I explore Blazor, Entity Framework Core, ASP.NET Core, and .NET 8.

Azure Blazor Site: [Azure Resume Site](https://marvinvalencia-bkcredfvbtd3cbgs.southeastasia-01.azurewebsites.net) <br/>
AWS Blazor Site: [AWS Resume Site](http://marvinvalencia.ap-southeast-1.elasticbeanstalk.com/) <br/>
ASP.Net Core: [Azure API in Swagger](https://resume-project-api-ama4f0fbfde9cxge.southeastasia-01.azurewebsites.net/swagger/index.html)<br/>

The aim is to build a full-stack web application while applying modern web development practices using the Microsoft ecosystem.

🚀 Goals & Feature Checklist <br/>
✅ = Done | 🔲 = To-do

🔧 Core Features & Architecture <br/>
✅ Setup ASP.NET Core Web API <br/>
✅ Setup Blazor WebAssembly Frontend <br/>
✅ Implement Clean Architecture (Domain, Application, Infrastructure, Web layers) <br/>
✅ Use Entity Framework Core for data access <br/>
✅ Add basic CRUD operations for resume/profile data <br/>
✅ Stylecop <br/>
✅ Use MediatR for CQRS pattern and separation of concerns <br/>

🖥️ Blazor WebAssembly Functionality <br/>
✅ Implement routing and navigation (e.g., /home, /projects, /contact) <br/>
✅ Add layout components (MainLayout, NavMenu, Footer) <br/>
🔲 Build form pages using EditForm, InputText, InputSelect, InputDate <br/>
🔲 Use model validation with DataAnnotations <br/>
🔲 Add real-time form validation feedback <br/>
🔲 Call protected endpoints using HttpClient with JWT bearer token <br/>
🔲 Handle loading, success, and error UI states (spinners, toast alerts) <br/>
🔲 Add file upload and image preview <br/>
🔲 Integrate with SignalR for real-time updates <br/>
🔲 Add global error handler and user-friendly error pages <br/>
🔲 Dark mode toggle with CSS classes <br/>
🔲 Export to PDF or Excel (e.g., resume download) <br/>

🛠️ Backend Enhancements <br/>
🔲 Integrate Redis for caching (e.g., user sessions, lookups) <br/>
🔲 Add Azure Service Bus (or RabbitMQ) for asynchronous messaging / background tasks <br/>
🔲 Setup SignalR for real-time updates (e.g., notifications, live edits) <br/>
🔲 Use BackgroundServices / Hosted Services for scheduled tasks or workers <br/>
🔲 Add support for global exception handling and structured logging <br/>
🔲 Use Serilog or Application Insights for logging and monitoring <br/>

🔐 Security & Secrets <br/>
✅ Add simple JWT Authentication and Authorization <br/>
✅ Store secrets using GitHub Secrets, secure app settings and API keys properly <br/>
✅ Show/hide content based on user roles (@attribute [Authorize(Roles = "Admin")]) <br/>

☁️ Deployment & CI/CD <br/>
✅ Deploy REST API (ASP.NET Core) for Resume entities <br/>
✅ Deploy Blazor WebAssembly App  <br/>
✅ Setup GitHub Actions for CI/CD pipeline <br/>
✅ Deploy to AWS (Elastic Beanstalk, ECS, or S3 + CloudFront for Blazor WASM) <br/>
✅ Store cloud-specific config and secrets using Azure App Settings or AWS Parameter Store <br/>
✅ Trigger GitHub Actions only when there are specific changes to a project <br/>
🔲 Dockerize API and Blazor apps for consistent builds and multi-cloud support <br/>

🧪 Testing <br/>
🔲 Add unit tests for application services <br/>
🔲 Add integration tests for API endpoints <br/>

🤖 AI / Assistant Features
✅ Integrate a basic AI chatbot (UI + backend service)
✅ Use GroqCloud API for responses
✅ Add a Blazor component for live chat
🔲 Store or log user prompts for context/training
🔲 Use PostgreSQL with pgvector extension for AI/RAG-based features

📌 Tech Stack <br/>
Frontend: Blazor WebAssembly /<br/>
Backend: ASP.NET Core (.NET 8) <br/>
Database: Azure SQL using Entity Framework Core <br/>
Authentication: JWT <br/>
DevOps: GitHub Actions, Azure App Service <br/>
