$ReportLocation = (pwd).path + "\bin\Debug\net6.0\allure-results";

WRITE-OUTPUT "Generating allure-report for the e2e tests..."
allure generate "$ReportLocation" --clean

WRITE-OUTPUT "Rebuild docker compose image..."
docker-compose build --no-cache

WRITE-OUTPUT "Running docker compose... Please open browser on page htttp://localhost:9999/index.html when run is finished"
docker-compose up