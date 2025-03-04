# **ImageUploadApp**  

A **.NET Core MVC** application that allows users to upload images with text descriptions, view uploaded entries, and edit them. **MongoDB with GridFS** is used for efficient image storage and retrieval.

---

## **Features**  

✅ **Upload Images with Text** – Users can upload an image along with a text description.  
✅ **View Uploaded Entries** – A separate page displays all previously uploaded images and texts.  
✅ **Edit Existing Entries** – Users can update text and replace the image.  
✅ **GridFS Integration** – Images are stored in MongoDB using GridFS for efficient storage.  
✅ **PNG & JPEG Support** – The app supports both `.png` and `.jpeg` image formats.    

---

## **Tech Stack**  

- **Backend:** .NET Core MVC  
- **Frontend:** Razor Views (CSHTML), Bootstrap  
- **Database:** MongoDB with GridFS  
- **Storage:** MongoDB GridFS (for images)  
- **Dependency Injection:** .NET Core Services  
- **IDE:** Visual Studio  

---

## **Installation & Setup**  

### **Prerequisites**  
✔ **MongoDB Installed & Running**  
✔ **.NET Core SDK Installed**  
✔ **Visual Studio Installed**  

### **Clone the Repository**  
```sh
git clone https://github.com/Saubhik998/ImageUploadApp.git
cd ImageUploadApp
```

### **Set Up MongoDB Connection**  
Modify `appsettings.json` to configure MongoDB connection:  
```json
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "ImageUploadDB",
  "GridFsBucket": "uploads"
}
```

### **Run the Application**  
```sh
dotnet run
```
Or run from **Visual Studio** by pressing **F5**.  

---

## **Usage Guide**  

### **1️⃣ Upload an Image & Text**  
1. Go to **"Upload"** page.  
2. Enter a **text description**.  
3. Choose an **image file** (PNG/JPEG).  
4. Click **"Upload"**.  

### **2️⃣ View Previous Uploads**  
- Click **"View Uploads"** button on the homepage.  
- You will see a list of **uploaded images** with text descriptions.  

### **3️⃣ Edit an Upload**  
- Click **"Edit"** next to an upload.  
- Modify the **text** or replace the **image**.  
- Click **"Save Changes"**.  

---

## **Project Structure**  

```
ImageUploadApp/
│-- Controllers/
│   ├── HomeController.cs
│-- Models/
│   ├── ImageTextModel.cs
│-- Services/
│   ├── GridFsService.cs
│-- Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Upload.cshtml
│   │   ├── ViewUploads.cshtml
│   │   ├── Edit.cshtml
│-- appsettings.json
│-- Program.cs

```

---

## **API Endpoints**  

| **Endpoint**            | **Method** | **Description**                                  
|-------------------------|------------|-----------------------------------------------
| `/Home/Index`           | GET        | Displays the homepage                           
| `/Home/Upload`          | GET        | Shows upload form                               
| `/Home/Upload`          | POST       | Uploads an image with text to MongoDB GridFS   
| `/Home/ViewUploads`     | GET        | Lists all uploaded images & texts              
| `/Home/Edit/{id}`       | GET        | Opens the edit page for a specific upload      
| `/Home/Edit`            | POST       | Updates an existing upload                     
| `/Home/GetImage/{id}`   | GET        | Retrieves an image by ID from GridFS           





