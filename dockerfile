FROM mcr.microsoft.com/windows:1809
RUN ["DISM", "/online", "/enable-feature", "/featurename:iis-webserver",  "/ALL"]
RUN powershell -Command New-Item -Path c:\docker -ItemType Directory -Force
ADD \Deployment C:\\Deployment
WORKDIR C:\\Deployment
RUN powershell -Command .\\data.ps1
