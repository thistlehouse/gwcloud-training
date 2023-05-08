# My Store API
- [MyStore API](#my-store-store-api)
    - [Client](#client)
        - [Create Client Request](#create-client-request)
        - [Get Client Request](#get-client-request)
        - [Get Clients Request](#get-clients-request)
        - [Update Client Request](#update-client-request)
        - [Delete Client Request](#delete-client-request)
    - [Product](#product)
        - [Create Product Request](#create-product-request)
        - [Get Product Request](#get-product-request)
        - [Get Products Request](#get-products-request)
        - [Update Product Request](#update-product-request)        
    - [Order](#order)
        - [Create Order Request](#create-order-request)
        - [Get Order Request](#get-order-request)
        - [Get Orders Request](#get-orders-request)
        - [Update Order Request](#update-order-request)

## Client
### Create Client Request
```js
POST api/client/new
```

### Get Clients Request
```js
POST api/client/clients
```

### Get Client Request
```js
GET api/client/client
```

### Update Client Request
```js
PUT api/client/update
```

### Delete Client Request
```js
DELETE api/client/delete
```

## Product
### Create Product Request
```js
POST api/product/new
```

### Get Products Request
```js
POST api/product/products
```

### Get Product Request
```js
POST api/product/product
```

### Update Product Request
```js
PUT api/product/update
```

## Order
### Create Order Request
```js
POST api/order/new
```

### Get Orders Request
```js
POST api/order/orders
```

### Get Order Request
```js
POST api/order/order
```

### Update Order Request
```js
POST api/order/products
```

### Delete Order Product Request
```js
DELETE api/order/products/remove
```

