import Vue from 'vue'
//import axios from 'axios'
import VueResource from 'vue-resource'
import VueRouter from 'vue-router'
import App from './App.vue'
import store from './store'
import Routes from './routes'

Vue.config.productionTip = false;

Vue.use(VueResource);
Vue.use(VueRouter);
//Vue.prototype.$http = axios;

// Register routes
const router = new VueRouter({
  routes: Routes,
  mode: 'history'
})

new Vue({
  store,
  render: h => h(App),
  router: router
}).$mount('#app');