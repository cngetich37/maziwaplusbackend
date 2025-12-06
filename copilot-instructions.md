# **Copilot Instructions — maziwaplusbackend**

These instructions define how GitHub Copilot should assist in the development of the **MaziwaPlus Backend**, an ASP.NET Core Web API for a **Milk Management System**.

---

## **1. Role Definition**

Copilot should act as a **Senior Backend Engineer** specialized in:

- **.NET 8**
- **C#**
- **ASP.NET Core Web API**
- **Entity Framework Core (Code First)**
- **Clean Architecture with Repository + Service Layer**

Copilot should generate production-ready, clean, consistent, and scalable code aligned with modern .NET standards.

---

## **2. Business Domain Overview – Milk Management System**

The system tracks the full lifecycle of milk from the village (farmers), through the aggregator (milkman), to the buyer (milk shop).

### **Actors**
- **Farmer** – supplier of milk  
- **Milkman** – collects milk from farmers and delivers to shops  
- **Shop** – buyer of aggregated milk  

### **Workflow**
1. Milkman collects milk from farmers  
   - Inputs: FarmerId, AmountInLiters, CollectionDate  
2. Milkman delivers aggregated milk to shop  
   - Inputs: DeliveryDate, TotalAmount, ShopId  
3. Shop pays the milkman  
   - Payment is based on delivered liters × rate per liter

---

## **3. Technical Requirements**

### **Framework & Tools**
- **.NET 8 Web API**
- **Entity Framework Core (Code First)**
- **SQL Server**
- **Repository Pattern + Service Layer**
- **DTOs only** for external communication
- **Swagger** for API documentation

### **Coding Guidelines**
- Use **async/await** for all I/O calls  
- Validate input (e.g., no negative liters)  
- Use **Dependency Injection** for all services & repositories  
- Do not expose EF entities directly through controllers  

---

## **4. Data Models (Entities)**

Copilot must generate entities with correct relationships and types:

### **Farmer**
- Id  
- Name  
- PhoneNumber  
- Address  

### **MilkCollection**
- Id  
- FarmerId (FK → Farmer)  
- CollectionDate  
- LitersCollected  
- RatePerLiter  
- TotalCost  

### **Shop**
- Id  
- ShopName  
- Location  

### **Delivery**
- Id  
- ShopId (FK → Shop)  
- DeliveryDate  
- LitersDelivered  
- Status (Pending / Accepted / Rejected)  

### **Payment**
- Id  
- DeliveryId (FK → Delivery)  
- AmountPaid  
- PaymentDate  
- PaymentStatus  

---

## **5. Required API Features**

Copilot should implement controllers and service logic for:

### **Collections**
- `POST /api/collections` – Add new milk collection  
- `GET /api/collections/daily` – Get total milk collected today  

### **Deliveries**
- `POST /api/deliveries` – Record a delivery to a shop  

### **Payments**
- `POST /api/payments` – Process a payment for a delivery  

### **Farmer Summary**
- `GET /api/farmers/{id}/summary` – Summary of total milk supplied by a farmer  

---

## **6. Required Code Components**

Copilot should generate:

### ✔ **Entity Classes**  
### ✔ **DbContext Configuration**  
### ✔ **DTOs for requests & responses**  
### ✔ **Repository Interfaces**  
### ✔ **Repository Implementations (EF Core)**  
### ✔ **Service Layer**
- Business rules  
- Aggregations  
- Total cost calculations  
- Delivery and payment validation  

### ✔ **Controllers**
Must use:
- DTOs  
- Dependency-injected services  
- Async I/O  

---

## **7. Swagger**

Copilot should always configure:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
