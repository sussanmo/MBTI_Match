# **MBTI Activity Matchmaker**

## **Overview**
The **MBTI Activity Matchmaker** is a simple web application built using ASP.NET Razor Pages. It allows users to select their MBTI type from a dropdown and input an activity. The application then randomly generates an MBTI type that best matches the selected activity and MBTI type. This project utilizes HTML, CSS, JavaScript, and C# and is containerized using Docker for deployment on an Azure Kubernetes Service (AKS) cluster.

## **Features**
- **Dropdown Selection**: Choose your MBTI type from a pre-defined list.
- **Activity Input**: Enter an activity that you are interested in.
- **Random Match Generation**: The app generates an MBTI type that matches your selected MBTI and activity.
- **Responsive UI**: Built with a combination of HTML, CSS, and JavaScript for a clean and user-friendly interface.
- **Dockerized**: The application is containerized using Docker for consistent deployment across environments.
- **AKS Deployment**: Deployed on an Azure Kubernetes Service (AKS) cluster for scalability and reliability.

## **Technologies Used**
- **ASP.NET Core Razor Pages**
- **C#**
- **HTML5**
- **CSS3**
- **JavaScript**
- **Docker**
- **Azure Kubernetes Service (AKS)**

## **Getting Started**

### **Prerequisites**
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- An Azure account with AKS configured

### **Installation**

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/mbti-activity-matchmaker.git
   cd mbti-activity-matchmaker
   
### ** Build the Application **
To build the application, run the following command:

dotnet build

### **Run Locally**
To run the application locally, use the command:

dotnet run

After running the command, open your browser and navigate to http://localhost:5000.

**Dockerization**
Build Docker Image
To build the Docker image, execute:

docker build -t mbti-activity-matchmaker .

**Run Docker Container Locally**
To run the Docker container locally, execute:

docker run -d -p 8080:80 mbti-activity-matchmaker

After running the command, open your browser and navigate to http://localhost:8080.

**Deployment to AKS**
Push Docker Image to Azure Container Registry (ACR)
To push the Docker image to Azure Container Registry, run the following commands:

Login to ACR:

az acr login --name <your-acr-name>

Tag the Docker image:

docker tag mbti-activity-matchmaker <your-acr-name>.azurecr.io/mbti-activity-matchmaker:v1

**Push the image to ACR:**

docker push <your-acr-name>.azurecr.io/mbti-activity-matchmaker:v1

**Deploy to AKS**
Ensure you have a Kubernetes context set up for your AKS cluster. Apply your Kubernetes manifests to deploy the app by running:

kubectl apply -f kubernetes/deployment.yaml

kubectl apply -f kubernetes/service.yaml

Accessing the Application
Once deployed, you can access the application using the external IP of the AKS service.
