<template>
    <div id="app">
        <app-header></app-header>
        <router-view></router-view>
    </div>
</template>

<script>
// Imports
import header from './components/header.vue';

export default {
    name: 'app',
    components: {
        'app-header': header
    },
    data () {
        return {

        }
    },
    methods: {

    }
}
</script>

<style>
body{
    margin: 0;
    font-family: 'Nunito SemiBold';
}
button{
    display: block;
    margin: 20px 0;
    background: crimson;
    color: #fff;
    border: 0;
    padding: 14px;
    border-radius: 4px;
    font-size: 18px;
    cursor: pointer;
}
</style>
