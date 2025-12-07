# Tasman Project

This is the Tasman ASP.NET Core project. Follow the instructions below to set up and run it on Mac or Windows.

---

## 1️⃣ Prerequisites

* Install **.NET 8 SDK** (includes runtime + scaffolding tools):

[Download .NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Check installation:

```bash
dotnet --version
```

It should show **8.0.x**.

---

## 2️⃣ Restore NuGet packages

If there are new or missing packages:

```bash
dotnet restore
```

This will download all dependencies listed in `Tasman.csproj`.

---

## 3️⃣ Build and run the project

```bash
dotnet build
dotnet run
```

The app should start, and you can access it locally.

---

## 4️⃣ Scaffolding Controllers (Optional)

If you want to generate new controllers:

```bash
dotnet aspnet-codegenerator controller -name MyController -outDir Controllers
```

* Only works if **.NET 8 SDK** is installed.
* Generates **controller only**, no views or models.

test test,test test