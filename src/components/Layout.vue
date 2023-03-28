<template>
    <div>
        <NavigationBar />
        <div>
            <slot></slot>
        </div>
        <HomePage></HomePage>
        <div>
            <slot></slot>
        </div>
    </div>
</template>

<script>
    import NavigationBar from './NavigationBar.vue';
    import HomePage from './HomePage.vue';

    export default {
        components: {
            NavigationBar,
            HomePage
        }
    };
</script>

<style lang="scss" scoped>
    
</style>