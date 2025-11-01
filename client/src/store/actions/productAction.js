import axios from 'axios';
import {
  GET_ERRORS,
  CLEAR_ERRORS,
  GET_PRODUCTS,
  GET_PRODUCT,
  ADD_PRODUCT,
  DELETE_PRODUCT,
  PRODUCT_LOADING
} from './type';

// Get product list
export const getProductPaginateByCategory = (
  searchKey,
  categoryId,
  currentPage
) => dispatch => {
  dispatch(clearErrors());
  dispatch(setProductLoading());
  
  // Build query parameters for .NET API
  const params = new URLSearchParams();
  if (searchKey && searchKey.trim()) params.append('searchTerm', searchKey);
  if (categoryId && categoryId !== null && categoryId !== 'null') params.append('categoryId', categoryId);
  params.append('page', currentPage);
  params.append('pageSize', 10);
  
  axios
    .get(`/api/products?${params.toString()}`)
    .then(res =>
      dispatch({
        type: GET_PRODUCTS,
        payload: res.data
      })
    )
    .catch(err =>
      dispatch({
        type: GET_ERRORS,
        payload: err.response.data
      })
    );
};

// export const getProducts = () => dispatch => {
//   dispatch(clearErrors());
//   dispatch(setProductLoading());
//   axios
//     .get('/api/products')
//     .then(res =>
//       dispatch({
//         type: GET_PRODUCTS,
//         payload: res.data
//       })
//     )
//     .catch(err =>
//       dispatch({
//         type: GET_PRODUCTS,
//         payload: []
//       })
//     );
// };

// Get product by category
export const getProductsByCategory = categoryId => dispatch => {
  dispatch(clearErrors());
  dispatch(setProductLoading());
  
  // Use the products endpoint with categoryId parameter
  const params = new URLSearchParams();
  if (categoryId && categoryId !== 'all') {
    params.append('categoryId', categoryId);
  }
  params.append('page', 1);
  params.append('pageSize', 100); // Get more products for category view
  
  axios
    .get(`/api/products?${params.toString()}`)
    .then(res =>
      dispatch({
        type: GET_PRODUCTS,
        payload: res.data
      })
    )
    .catch(err =>
      dispatch({
        type: GET_PRODUCTS,
        payload: []
      })
    );
};

// Get product info
export const getProduct = id => dispatch => {
  dispatch(setProductLoading());
  axios
    .get(`/api/products/${id}`)
    .then(res =>
      dispatch({
        type: GET_PRODUCT,
        payload: res.data
      })
    )
    .catch(err =>
      dispatch({
        type: GET_PRODUCT,
        payload: {}
      })
    );
};

// Add product
export const addProduct = (productData, history) => dispatch => {
  return new Promise((resolve, reject) => {
    let dataForm = new FormData();

    dataForm.append('photo', productData.image);
    dataForm.append('name', productData.name);
    dataForm.append('price', productData.price);
    dataForm.append('quantity', productData.quantity);
    dataForm.append('note', productData.note);
    dataForm.append('category', productData.category);

    // for (let item of dataForm.entries()) {
    //   console.log('item form', item);
    // }

    axios
      .post('/api/products', dataForm)
      .then(res => {
        dispatch({
          type: ADD_PRODUCT,
          payload: res.data
        });
        history.push('/manage-products');
        resolve();
      })
      .catch(err => {
        dispatch({
          type: GET_ERRORS,
          payload: err.response.data
        });
        reject(err.response.data);
      });
  });
};

// Edit product
export const editProduct = (id, productData, history) => dispatch => {
  let dataForm = new FormData();

  dataForm.append('photo', productData.image);
  dataForm.append('name', productData.name);
  dataForm.append('price', productData.price);
  dataForm.append('quantity', productData.quantity);
  dataForm.append('note', productData.note);
  dataForm.append('category', productData.category);

  // for (let item of dataForm.entries()) {
  //   console.log('item form', item);
  // }

  return new Promise((resolve, reject) => {
    axios
      .put(`/api/products/${id}`, dataForm)
      .then(res => {
        // dispatch({
        //   type: EDIT_PRODUCT,
        //   payload: res.data
        // });

        history.push('/manage-products');
        resolve();
      })
      .catch(err => {
        dispatch({
          type: GET_ERRORS,
          payload: err.response.data
        });
        reject(err.response.data);
      });
  });
};

//Delete product
export const deleteProduct = id => dispatch => {
  return new Promise((resolve, reject) => {
    axios
      .delete(`/api/products/${id}`)
      .then(res => {
        dispatch({
          type: DELETE_PRODUCT,
          payload: id
        });
        resolve();
      })
      .catch(err => {
        dispatch({
          type: GET_ERRORS,
          payload: err.response.data
        });
        reject(err.response.data);
      });
  });
};

export const setProductLoading = () => {
  return {
    type: PRODUCT_LOADING
  };
};

// Clear errors
export const clearErrors = () => {
  return {
    type: CLEAR_ERRORS
  };
};
