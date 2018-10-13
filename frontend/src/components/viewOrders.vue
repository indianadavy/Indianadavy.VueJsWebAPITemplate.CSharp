<template>
    <div id="view-Orders">
        <h2>Orders Per User</h2>
        <label>Selecte a User</label>
        <select v-model="selected">
            <option selected='' v-for="(user,index) in users" :key="index" v-bind:value="user.id">
                {{ user.firstName + ' ' + user.lastName }}
            </option>
        </select>
        <div v-if="selected">
            <div v-for="(order, index) in orders" :key="index" class="single-order">
                <router-link v-bind:to="'/order/' + order.id"><h2>Order #{{ order.id }} - Tracking ID #{{ order.trackingID }} - {{ order.name }}</h2></router-link>
            </div>
            <h3 v-if="orders.length < 1">User Does not have any orders</h3>
        </div>
    </div>
</template>

<script>
export default {
    data () {
        return {
            selected: ''
        }
    },
    computed: {
        users() {
            return this.$store.state.users;
        },
        orders() {
            return this.$store.state.orders;
        }
    },
    watch: {
        selected: function() {
            this.getOrdersForUser();
        }
    },
    methods: {
        getOrdersForUser: function() {
            this.$store.dispatch('getOrdersForUser', this.selected);
        }
    },
    beforeCreate() {
        this.$store.dispatch('clearOrders');
        this.$store.dispatch('getUsers'); 
    }
}
</script>

<style scoped>
#view-Orders *{
    box-sizing: border-box;
}
#view-Orders{
    margin: 20px auto;
    max-width: 600px;
    padding: 20px;
}
label{
    display: block;
    margin: 20px 0 10px;
}
select{
    display: block;
    width: auto;
    padding: 8px;
}
h2{
    margin-top: 10px;
}
.single-order{
    width: 1000px;
}
</style>