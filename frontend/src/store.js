import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  //strict: process.env.NODE_ENV !== 'production',
  state: {
    user: {
      firstName: '',
      lastName: ''
    },
    users: [],
    order: {
      name: '',
      streetAddress: '',
      city: '',
      state: '',
      zipCode: '',
      trackingID: '',
    },       
    orders: [],
    ui: {
      user: { 
        submitted: false 
      },
      order: {
        submitted: false
      }
    }
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    setUsers(state, users) {
      state.users = users;
    },
    addUser(state, newUser) {
      state.users.push(newUser);
    },
    userFormSubmitted(state, newVal) {
      state.ui.user.submitted = newVal;
    },
    setOrders(state, orders) {
      state.orders = orders;
    },
    setOrder(state, newOrder) {
      state.order = newOrder;
    },
    addOrder(state, newOrder) {
      state.orders.push(newOrder);
    },
    orderFormSubmitted(state, newVal) {
      state.ui.order.submitted = newVal;
    }
  },
  actions: {
    resetForm({ commit }, type) {
      if(type == 'order') {
        commit('orderFormSubmitted', false);
        commit('setOrder', {});
      } else if(type == 'user') {
        commit('userFormSubmitted', false);
        commit('setUser', {});
      }
    },
    getUsers({ commit }) {
      Vue.http.get('http://localhost:5000/api/user').then(response => {
        commit('setUsers', response.data);
      })
    },
    createUser({ commit }, user) {
      return new Promise((resolve, reject) => {
        Vue.http.post('http://localhost:5000/api/user', user)
        .then(() => {
          commit('addUser', user);
          commit('userFormSubmitted', true);
          resolve(true);
        })
        .catch(error => {
          reject(error);
        });
      })
    },
    getOrdersForUser({ commit }, userID) {
      Vue.http.get('http://localhost:5000/api/order/forUser/' + userID).then(response => {
        commit('setOrders', response.data);
      })
    },
    getOrder({ commit }, orderID) {
      Vue.http.get('http://localhost:5000/api/order/' + orderID).then(response => {
        commit('setOrder', response.data);
      })
    },
    createOrder({ commit }, order) {
      return new Promise((resolve, reject) => {
        Vue.http.post('http://localhost:5000/api/order', order)
        .then(() => {
          commit('addOrder', order);
          commit('orderFormSubmitted', true);
          resolve(true);
        })
        .catch(error => {
          reject(error);
        });
      })
    },
    // if (!this.order.trackingID == '')
    //         {
    //             this.order.userID = this.selected;
    //             this.$http.post('http://localhost:5000/api/order', this.order).then(function(){
    //                 this.submitted = true;
    //                 this.selected = '';
    //             });
    //         }
    //         else {
    //             // TODO: What do we do here?
    //             // console.errors('missing info');
    //         }

    updateOrder({ commit }, order) {
      return new Promise((resolve, reject) => {
        Vue.http.put('http://localhost:5000/api/order', order)
        .then(() => {
          commit('setOrder', order);
          commit('orderFormSubmitted', true);
          resolve(true);
        })
        .catch(error=> {
          reject(error);
        });
      })   
    }
  }
});