# Winedonia - Macedonian Wines Web App

**Winedonia** is a sophisticated .NET web application that celebrates the rich and diverse world of Macedonian wines and wineries. This repository contains the source code for the web app, which follows a microservice architecture consisting of two integral components: the main app and a powerful search API.

It currently supports only **Macedonian**, however, there are plans to localize text for English as well.

## Microservices Architecture:

- **Main App:** This microservice serves as the core of **Winedonia**, leveraging SQL Server as its database with EF Core. It ensures a seamless browsing experience, allowing users to explore detailed information about various Macedonian wines and wineries.

- **Search API:** The search API microservice significantly enhances user experience through quick and efficient search functionality. It communicates with the main app through gRPC, facilitating seamless data retrieval and interaction. The search API is integrated with an Elasticsearch cluster, which serves as a robust search engine. This empowers users to filter through the extensive catalog effortlessly.

## Technologies Used:

- **Database:** SQL Server, managed with Entity Framework Core (EF Core), forms the backbone of the main app, ensuring reliable data storage and retrieval.

- **Search Engine:** Elasticsearch, integrated with the Search API, enhances the platform's search capabilities. It efficiently indexes and retrieves data related to wines and wineries for quick and accurate searches.

- **Frontend:** The user interface is built using React, providing a responsive and engaging experience for users exploring Macedonian wines and wineries.

- **Containerization:** Both the SQL Server database and Elasticsearch cluster, along with other components, are hosted in Docker containers. The containers are orchestrated and managed using Docker Compose, streamlining the deployment process and ensuring consistency across different environments.

## Features:

- **Comprehensive Catalog:** Explore a vast catalog of Macedonian wines, each accompanied by detailed descriptions, tasting notes, and pairing suggestions.

- **Winery Profiles:** Dive into profiles of various wineries, uncovering their rich history, unique winemaking techniques, and the diverse range of wines they offer.

- **Efficient Search and Filters:** Utilize the powerful search capabilities provided by the Elasticsearch cluster to quickly find specific wines or wineries. Apply filters based on varietals, regions, or wine types to refine your search results.

- **User Reviews:** Engage with the community by sharing your experiences and reviews of Macedonian wines. Contribute to the platform's dynamic environment by offering insights and recommendations to fellow wine enthusiasts.

- **Responsive Design:** Enjoy a seamless browsing experience across devices, thanks to the responsive design that adapts to various screen sizes.

Whether you're a seasoned wine connoisseur or a curious novice, **Winedonia** offers a user-friendly platform to explore and appreciate the flavors and traditions unique to Macedonia's winemaking heritage.

Clone the repository and embark on a journey to discover the exquisite world of Macedonian wines with **Winedonia**!

## Original project repository link
https://github.com/antoniostefanovski/Software-Design-and-Architecture