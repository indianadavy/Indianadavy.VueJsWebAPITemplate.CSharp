<template>
    <div id="new-Order">
        <div v-if="!submitted">
            <h2>Create a New Order</h2>
            <label>Selecte a User</label>
            <select v-model="userSelected">
                <option userSelected='' v-for="(user,index) in users" :key="index" v-bind:value="user.id">
                    {{ user.firstName + ' ' + user.lastName }}
                </option>
            </select>
        </div>

        <form v-if="!submitted && userSelected" @submit.prevent="checkForm">
            <label>Name:</label>
            <input type="text" v-model.lazy="order.name" required />
            <label>Street Address:</label>
            <input type="text" v-model.lazy="order.streetAddress" required />  
            <label>City:</label>
            <input type="text" v-model.lazy="order.city" required />  
            <label>State:</label>
            <input type="text" v-model.lazy="order.state" required />  
            <label>Zip Code</label>
            <input type="text" v-model.lazy="order.zipCode" required />  
            <label>Tracking ID</label>
            <input type="text" v-model.lazy="order.trackingID" required /> 
            <button type="submit">Create Order</button>
        </form>
        <div v-if="submitted">
            <h3>Order #{{ order.trackingID }} created</h3>
            <button @click="resetForm">Add a new Order</button>
        </div>
    </div>
</template>

<script>
// Imports

export default {
    data () {
        return {
            userSelected: '',
            errors: []
        }
    },
    computed: {
        users() {
            return this.$store.state.users;
        },
        order(){
            return this.$store.state.order;
        },
        submitted(){
            return this.$store.state.ui.order.submitted;
        }
    },
    methods: {
        checkForm:function(e) {
            this.errors = [];
            if(!this.order.name) this.errors.push("Name required.");
            if(!this.order.streetAddress) this.errors.push("Street Address required.");
            if(!this.order.city) this.errors.push("City required.");
            if(!this.order.state) this.errors.push("State required.");
            if(!this.order.zipCode) this.errors.push("Zip Code required.");
            if(!this.order.trackingID) this.errors.push("Tracking ID required.");
            if(!this.errors.length){
                this.createOrder();
            }
            else{
                // TODO: What do we do here?
                // console.log("there are errors");
                e.preventDefault();
            } 
        },
        createOrder: function(){
            this.order.userID = this.userSelected;
            this.$store.dispatch('createOrder', this.order, this.selected);
        },
        reload: function() {    
            this.$router.go(this.$router.currentRoute)
        },
        resetForm: function() {
            this.$store.dispatch('resetForm', 'order');
        }
    },
    beforeCreate() {
        this.$store.dispatch('resetForm', 'order');
        this.$store.dispatch('getUsers'); 
    }
}
</script>

<style scoped>
#new-Order *{
    box-sizing: border-box;
}
#new-Order{ 
    margin: 20px auto;
    max-width: 600px;
    padding: 20px;
}
label{
    display: block;
    margin: 20px 0 10px;
}
input[type="text"]{
    display: block;
    width: 100%;
    padding: 8px;
}
select{
    display: block;
    width: auto;
    padding: 8px;
}
h2{
    margin-top: 10px;
}
</style>