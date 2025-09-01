export const environment = {
  production: false,
  apiBaseUrl: 'http://localhost:5000/api',
  //Test
  protectedEndpoints: [
    '/users',
    '/orders',
    '/payments'
  ]
};
