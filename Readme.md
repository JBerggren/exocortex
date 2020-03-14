# ExoCortex

This project is for storing of data and then for creating agents that can react to that data (think IFTTT).

### POST /input
Body: { "type": "{typeOfData}", "data":{{DataObject}}}  
Returns id of record if successful. Or 500 server error if something went worng

### Get /input
Optional arguments: **type** (to filter by), **limit** (nr of record to return)
Without argument it returns the latest saved record.  
An example of this is 
```
{
    "items": [
        {
            "id": "A1jfHg9p8VQvUeGVrCRc",
            "time": "2020-03-14T18:18:43.181749Z",
            "type": "gps",
            "data": {
                "lat": "60.2454574",
                "lng": "17.5790637"
            },
            "handledBy": null
            }
    ]
}
```

### Get /input/count
Simply returns the nr of stored records
  
### Get /execute?agent={agentName}
Executes a agent script. Looks for the file {agentName}.txt in the  **Agents** Folder and executes it. 

## Todo
Be able to create and edit existing agent scripts from a web interface.