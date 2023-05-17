# My Store API
- [MyStoreApi API](#my-store-store-api)
    - [Customer](#Customer)
        - [Create Customer Request](#create-Customer-request)
        - [Get Customer Request](#get-Customer-request)
        - [Get Customers Request](#get-Customers-request)
        - [Update Customer Request](#update-Customer-request)
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
        - [Remove Procuct](#delete-order-product-request)

## Customer
### Create Customer Request
```js
POST api/Customer/new
```

### Get Customers Request
```js
POST api/Customer/Customers
```

### Get Customer Request
```js
GET api/Customer/Customer
```

### Update Customer Request
```js
PUT api/Customer/update
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

