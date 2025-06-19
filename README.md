Hi, This is my personal learning project where I explore Blazor, Entity Framework Core, ASP.NET Core, and .NET 8.

The aim is to build a full-stack web application while applying modern web development practices using the Microsoft ecosystem.

🚀 Goals & Feature Checklist
✅ = Done | 🔲 = To-do

🔧 Core Features & Architecture
✅ Setup ASP.NET Core Web API
✅ Setup Blazor WebAssembly Frontend
✅ Implement Clean Architecture (Domain, Application, Infrastructure, Web layers)
✅ Use Entity Framework Core for data access
✅ Add basic CRUD operations for resume/profile data

🖥️ Blazor WebAssembly Functionality TODO
✅ Implement routing and navigation (e.g., /home, /projects, /contact)
✅ Add layout components (MainLayout, NavMenu, Footer)
🔲 Build form pages using EditForm, InputText, InputSelect, InputDate
🔲 Use model validation with DataAnnotations
🔲 Add real-time form validation feedback
🔲 Call protected endpoints using HttpClient with JWT bearer token
🔲 Handle loading, success, and error UI states (spinners, toast alerts)
🔲 Add file upload and image preview
🔲 Integrate with SignalR for real-time updates
🔲 Add global error handler and user-friendly error pages
🔲 Dark mode toggle with CSS classes
🔲 Export to PDF or Excel (e.g., resume download)

🛠️ Backend Enhancements (Planned)
🔲 Integrate Redis for caching (e.g., user sessions, lookups)
🔲 Add Azure Service Bus (or RabbitMQ) for asynchronous messaging / background tasks
🔲 Setup SignalR for real-time updates (e.g., notifications, live edits)
🔲 Use BackgroundServices / Hosted Services for scheduled tasks or workers
🔲 Add support for global exception handling and structured logging
🔲 Use Serilog or Application Insights for logging and monitoring

🔐 Security & Secrets
✅ Add simple JWT Authentication and Authorization
✅ Store secrets using GitHub Secrets, secure app settings and API keys properly
🔲 Show/hide content based on user roles (@attribute [Authorize(Roles = "Admin")])

☁️ Deployment & CI/CD
✅ Deploy REST API (ASP.NET Core) for Resume entities
✅ Deploy Blazor WebAssembly App 
✅ Setup GitHub Actions for CI/CD pipeline
🔲 Dockerize API and Blazor apps for consistent builds and multi-cloud support
🔲 Deploy to AWS (Elastic Beanstalk, ECS, or S3 + CloudFront for Blazor WASM)
🔲 Store cloud-specific config and secrets using Azure App Settings or AWS Parameter Store

🧪 Testing
🔲 Add unit tests for application services
🔲 Add integration tests for API endpoints

📌 Tech Stack
Frontend: Blazor WebAssembly
Backend: ASP.NET Core (.NET 8)
Database: Azure SQL using Entity Framework Core
Authentication: JWT 
DevOps: GitHub Actions, Azure App Service 
