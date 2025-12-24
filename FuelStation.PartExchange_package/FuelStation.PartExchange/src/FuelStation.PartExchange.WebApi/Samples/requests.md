# Sample requests and JWT usage

1) Get a token (test-only) — Operator

POST /api/auth/token

Body (JSON):

{
  "StationId": "<requesting-station-guid>",
  "Role": "Operator"
}

Response: { "token": "..." }

Example cURL to get token:

```bash
curl -X POST "https://localhost:5001/api/auth/token" -H "Content-Type: application/json" -d '{"StationId":"<station-guid>","Role":"Operator"}'
```

2) Create Part Request (as Operator)

POST /api/partrequests

Headers: `Authorization: Bearer <token>`

Body:

{
  "RequestingStationId": "<station-guid>",
  "PartNumber": "P-100",
  "Quantity": 1
}

cURL example:

```bash
curl -X POST "https://localhost:5001/api/partrequests" -H "Content-Type: application/json" -H "Authorization: Bearer <token>" -d '{"RequestingStationId":"<station-guid>","PartNumber":"P-100","Quantity":1}'
```

3) Confirm Order (as Supplier)

Assuming an `Order` was created by the service and you have its `Id`.

POST /api/orders/{id}/confirm

Headers: `Authorization: Bearer <supplier-token>` (Role = Supplier)

cURL example:

```bash
curl -X POST "https://localhost:5001/api/orders/<order-id>/confirm" -H "Authorization: Bearer <supplier-token>"
```

Notes:
- Use the sample data seeded at startup: two stations in `Tehran` and one `Part` with PartNumber `P-100` available at `Station B` with quantity 10. Create token with `StationId` set to seeded station GUID (check DB) or create a station and use its Id.
- If you run the API locally the URL may be `http://localhost:5000` or `https://localhost:5001` depending on Kestrel output.
