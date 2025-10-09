import azure.functions as func
import logging
import json

app = func.FunctionApp(http_auth_level=func.AuthLevel.ANONYMOUS)


@app.route(route="HttpExample")
def HttpExample(req: func.HttpRequest) -> func.HttpResponse:
    """
    Accepts name, email, and age from query string or JSON body.

    Rules applied:
    - Title-case the name
    - Lowercase the email
    - Convert age to integer if possible, otherwise return "not provided"
    - Use sensible defaults if missing
    """
    logging.info('Python HTTP trigger function processed a request.')

    # Read from query string first
    name = req.params.get('name')
    email = req.params.get('email')
    age = req.params.get('age')

    # If any param is missing, try reading from JSON body
    if not (name and email and age):
        try:
            req_body = req.get_json()
        except ValueError:
            req_body = {}

        if not name:
            name = req_body.get('name')
        if not email:
            email = req_body.get('email')
        if not age:
            age = req_body.get('age')

    # Apply sensible defaults and transformations
    # Name: Title-case
    if name:
        try:
            name_processed = str(name).strip().title()
        except Exception:
            name_processed = "Anonymous"
    else:
        name_processed = "Anonymous"

    # Email: lowercase
    if email:
        try:
            email_processed = str(email).strip().lower()
        except Exception:
            email_processed = "unknown"
    else:
        email_processed = "unknown"

    # Age: try to convert to int, otherwise "not provided"
    age_processed = "not provided"
    if age is not None:
        try:
            # Accept numeric types and numeric strings
            if isinstance(age, (int, float)):
                age_processed = int(age)
            else:
                age_str = str(age).strip()
                # empty string -> treat as not provided
                if age_str == "":
                    age_processed = "not provided"
                else:
                    age_processed = int(age_str)
        except Exception:
            age_processed = "not provided"

    result = {
        "name": name_processed,
        "email": email_processed,
        "age": age_processed,
    }

    return func.HttpResponse(body=json.dumps(result), status_code=200, mimetype="application/json")