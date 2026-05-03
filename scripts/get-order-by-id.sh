#!/bin/bash

API_URL="http://localhost:5260"

if [ -z "$1" ]; then
  echo "Usage: ./scripts/get-order-by-id.sh <order-id>"
  exit 1
fi

curl "$API_URL/api/orders/$1"