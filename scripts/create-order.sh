#!/bin/bash

API_URL="http://localhost:5260"

curl -X POST "$API_URL/api/orders" \
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