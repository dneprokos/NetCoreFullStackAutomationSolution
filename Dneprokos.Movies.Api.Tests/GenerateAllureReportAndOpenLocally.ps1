$ReportLocation = (pwd).path + "\bin\Debug\net6.0\allure-results";

WRITE-OUTPUT "Generating allure-report for the e2e tests..."
allure generate "$ReportLocation" --clean

WRITE-OUTPUT "Opening allure server..."
allure open "allure-report" --port 9999