# ExoCortex

This project is for storing of data and then for creating agents that can react to that data (think IFTTT).

## Input 

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

## Agents
Agents are stored as  {agentName}.txt in the  **Agents** Folder.

### GET /agent
List of all agents. 
```
{"agents":["LastLocation"]}
```

### GET /agent/{agent}
Returns code for agent *agent*.

### POST /agent/{agent}
Update code for agent *agent*

### DELETE /agent/{agent}
Delete agent *agent*

### Get /agent/{agent}/execute
Executes a agent script and returns whatever the script returns

## Todo
Create CRUD web interface for agents.