<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-6 col-sm-6 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="media d-flex">
                                    <div class="media-body">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <h4>Hello, John Canbers</h4>
                                            <h4><span class="badge rounded-pill bg-success">Full access</span></h4>
                                        </div>
                                        <span>welcome to the <span class="fw-bold text-success">doctor</span> page</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-6 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="media d-flex">
                                    <div class="media-body text-right">
                                        <span>
                                        <i class='bx bxs-info-square card-icon'></i>
                                            Last logon at 16:56:20 13/10/2023 with
                                            <span class="badge ms-2 rounded-pill bg-success">MFA</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
    export default {
        methods: {
            formatDate(date) {
                return moment(date).format("HH:mm:ss DD/MM/YYYY");
            },
        },
        computed: {
            user() {
                return this.$store.state.user;
            },
        },
    };
</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";
    h4 {
        padding-right: 40px;
    }
    .card-icon {
        font-size: 1.5rem;
        color: $main-blue-bright;
        padding-right: 10px;
        position: relative;
        top: 5px;
    }
</style>