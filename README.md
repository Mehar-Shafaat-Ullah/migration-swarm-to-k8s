# Swarm-to-Kubernetes Migration Project

This project demonstrates the migration of a Full-Stack application (React, .NET Core, MSSQL) from Docker Swarm to a Production-Ready Kubernetes Cluster.

## 🚀 Architecture
- **Frontend:** React.js hosted via Nginx.
- **Backend:** .NET 8 API with Entity Framework Core.
- **Database:** Microsoft SQL Server (MSSQL).
- **Ingress:** Nginx Ingress Controller with SSL/TLS termination.

## 🛠️ Key Features
- **Path-Based Routing:** `/` routes to Frontend, `/api` routes to Backend.
- **SSL Security:** Secured via Kubernetes Secrets and TLS.
- **Service Discovery:** Internal communication via ClusterIP Services.
- **Database Persistence:** Automated migrations via .NET EF Core.

## 📦 How to Deploy
1. **Create SSL Secret:**
   `kubectl create secret tls myapp-tls-secret --cert=server.crt --key=server.key`
2. **Apply Manifests:**
   `kubectl apply -f k8s/`
3. **Set Environment Variables:**
   `kubectl set env deployment/dotnet-api ConnectionStrings__DefaultConnection='...'`

## 🔗 Access
- **URL:** `https://swarm-dp.local`
- **API Status:** `https://swarm-dp.local/api/status`
