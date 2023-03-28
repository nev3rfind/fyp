<template>
    <div>
        <NavigationBar />
        <div>
            <!-- Add a slot element here -->
            <slot></slot>
        </div>
    </div>
</template>

<script>
    import NavigationBar from './NavigationBar.vue';

    export default {
        components: {
            NavigationBar
        }
    };
</script>

<style lang="scss" scoped>
    
</style>