import json, shutil, os

TEMP_FILE_PATH = 'appsettings.json'
COPY_DESTINATIONS = [
    'TourPlanner/TourPlanner.API/appsettings.json',
    'TourPlanner/TourPlanner.Tests/appsettings.json'
]

connection_string = input('enter connection string:')
map_quest_key = input('input MapQuest key:')

config = {
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        "TestDbContext": connection_string
    },
    "ApiKeys": {
        "MapQuestKey": map_quest_key
    }
}

with open(TEMP_FILE_PATH, 'w') as f:
    json.dump(config, f, indent=4)

for dst in COPY_DESTINATIONS:
    shutil.copyfile(TEMP_FILE_PATH, dst)

os.remove(TEMP_FILE_PATH)