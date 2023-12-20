# How api works?

## Step 1
Create a HTTP Get to API server of GPM-Login. GPM-Login app will open profile and return a json contains [driver_path, remote_debug_address]

## Step 2
Use [driver_path, remote_debug_address] to create connection between Selenium / Puppeteer lib and WebDriver.