<template>
    test
    <div>
        <h1>Hello, {{ user.FullName }}</h1>
        <!-- Rest of the home page content -->
    </div>

</template>

<script>
    export default {
        // ...
        computed: {
            user() {
                return this.$store.state.user;
            },
        },
    };
</script>

<style lang="scss" scoped>
</style>