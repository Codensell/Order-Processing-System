#!/bin/bash

curl -X POST http://localhost:5260/api/orders \
  -H "Content-Type: application/json" \
  -d '{
    "items": [
      {
        "productName": "Keyboard",
        "price": 100,
        "quantity": 1
      }
    ]
  }'