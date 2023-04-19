<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="title-container d-flex p-1 flex-column m-auto text-center pb-4">
                    <div class="px-2">
                        <h1>Appointments <i class='bx bxs-calendar'></i></h1>
                    </div>
                </div>
                <div class="col-xl-12 col-sm-12 col-md-12 col-12 pb-4">
                    <div class="card mb-2 title-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-6">
                                        <label for="start-date" class="form-label">Date range from</label>
                                        <input type="date" class="form-control border" id="start-date" v-model="startDate">
                                    </div>
                                    <div class="col-6">
                                        <label for="start-date" class="form-label">Date range to</label>
                                        <input type="date" class="form-control border" id="start-date" v-model="startDate">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <button class="btn confirm-btn w-100 appointment-btn"><i class='bx bx-calendar-plus me-2'></i> Schedule new appointment</button>
                    </div>
                </div>
            </div>
            <div class="row scrollable-container">
                <div class="col-xl-6 col-sm-12 col-md-12 col-12 mt-0">
                    <div class="card mb-2 appointment-card">
                        <div class="card-content">
                            <div class="card-body card-top ps-4">
                                <div class="top-card-text mb-2"><span class="badge bg-danger badge-app">Not attended</span></div>
                                <div class="top-card-text"><i class='bx bxs-time time-icon'></i> 12 January 2020, 8:35AM</div>
                            </div>
                            <div class="card-body card-down ps-4">
                                <div class="avatar-block">
                                    <div class="avatar-initials">JH</div>
                                </div>
                                <div class="card-down-content d-inline-block ps-3">
                                    <p class="mb-0">John <strong>Hopkins</strong></p>
                                    <p class="mb-2">22/12/1999</p>
                                    <div class="mb-3"><span class="badge border bg-app-name"><i class='bx bxs-info-circle'></i> Award integration executive</span></div>
                                </div>
                                <div class="d-flex buttons-row">
                                    <button class="btn btn-sm btn-success w-50">Modify</button>
                                    <button class="btn btn-sm btn-warning w-25 ms-2 me-2 text-dark">Cancel</button>
                                    <button class="btn btn-sm btn-danger w-25">Delete</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-12 col-md-12 col-12 mt-4">
                    <div class="card mb-2 appointment-card">
                        <div class="card-content">
                            <div class="card-body card-top ps-4">
                                <div class="top-card-text mb-2"><span class="badge bg-danger badge-app">Not attended</span></div>
                                <div class="top-card-text"><i class='bx bxs-time time-icon'></i> 12 January 2020, 8:35AM</div>
                            </div>
                            <div class="card-body card-down ps-4">
                                <div class="avatar-block">
                                    <div class="avatar-initials">JH</div>
                                </div>
                                <div class="card-down-content d-inline-block ps-3">
                                    <p class="mb-0">John <strong>Hopkins</strong></p>
                                    <p class="mb-2">22/12/1999</p>
                                    <div class="mb-3"><span class="badge border bg-app-name"><i class='bx bxs-info-circle'></i> Award integration executive</span></div>
                                </div>
                                <div class="d-flex buttons-row">
                                    <button class="btn btn-sm btn-success w-50">Modify</button>
                                    <button class="btn btn-sm btn-warning w-25 ms-2 me-2 text-dark">Cancel</button>
                                    <button class="btn btn-sm btn-danger w-25">Delete</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-12 col-md-12 col-12 mt-4 pb-5">
                    <div class="card mb-2 appointment-card">
                        <div class="card-content">
                            <div class="card-body card-top ps-4">
                                <div class="top-card-text mb-2"><span class="badge bg-danger badge-app">Not attended</span></div>
                                <div class="top-card-text"><i class='bx bxs-time time-icon'></i> 12 January 2020, 8:35AM</div>
                            </div>
                            <div class="card-body card-down ps-4">
                                <div class="avatar-block">
                                    <div class="avatar-initials">JH</div>
                                </div>
                                <div class="card-down-content d-inline-block ps-3">
                                    <p class="mb-0">John <strong>Hopkins</strong></p>
                                    <p class="mb-2">22/12/1999</p>
                                    <div class="mb-3"><span class="badge border bg-app-name"><i class='bx bxs-info-circle'></i> Award integration executive</span></div>
                                </div>
                                <div class="d-flex buttons-row">
                                    <button class="btn btn-sm btn-success w-50">Modify</button>
                                    <button class="btn btn-sm btn-warning w-25 ms-2 me-2 text-dark">Cancel</button>
                                    <button class="btn btn-sm btn-danger w-25">Delete</button>
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
    
</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";

    .title-container {
        i {
            position: relative;
            top: 3px;
            color: $main-blue-nice;
        }
    }
    .appointment-btn {
        background: $main-blue-nice !important;
        border-radius: 20px;
        font-size: 1.2rem;
        color: $neutral-white;

        i {
            position: relative;
            top: 1px;
        }
    }

    .calendar-icon {
        font-size: 18px;
        color: $main-blue-bright;
        position: relative;
        top: 1px;
        padding-right: 5px;
    }

    .user-icon {
        position: relative;
        top: 3px;
        font-size: 20px;
        color: $green;
    }
    .avatar-initials {
        font-family: Nunito;
        background-color: $neutral-grey-mid;
        color: #FFFFFF;
        text-align: center;
        line-height: 0px;
        border-radius: 30%;
        font-size: 28px;
        font-family: Nunito;
        padding: 22px 16px;
    }

    .btn-primary {
        background-color: #0080ff;
        border-color: #0080ff;
    }

    .btn-primary:hover {
        background-color: #0073e6;
        border-color: #0073e6;
    }

    .btn-danger {
        background-color: #ff4d4d;
        border-color: #ff4d4d;
    }

    .btn-danger:hover {
        background-color: #ff1a1a;
        border-color: #ff1a1a;
    }

    .appointment-card {
        border-radius: 20px;
    }

    .card-top {
        background-color: $main-blue-nice;
        border-radius: 20px 20px 0px 0px;
    }

    .card-down {
        background-color: #FFFFFF;
        border-radius: 0px 0px 20px 20px;
    }

    .top-card-text {
        color: $neutral-white;
    }

    .badge-app {
        font-size: .85rem;
        font-weight: 500;
    }

    .top-card-text:nth-child(2) {
        font-size: .95rem;
    }
    .time-icon {
        font-size: 20px;
        padding-right: 5px;
        position: relative;
        top: 3px;
    }
    .avatar-block {
        display: inline-block;
        vertical-align: top;
    }

    .card-down-content {

        p:nth-child(1) {
            color: $neutral-grey-dark;
        }

        p:nth-child(2) {
            font-size: 0.9rem;
            color: $neutral-black;
            font-weight: bold;
        }
    }

    .bg-app-name {
        color: $neutral-black;
        background: $neutral-grey-pale;
        font-size: .8rem;
        padding-bottom: 7px;

        i {
            font-size: 18px;
            color: $main-blue-bright;
            position: relative;
            top: 3px;
            padding-right:5px;
        }
    }

    .buttons-row {

        button {
        text-transform: uppercase;
        border-radius: 15px;
        color: $neutral-white;
        padding: 8px;
        font-size: .8rem;
        }
        button:nth-child(1) {
            background: $main-blue-nice!important;
        }
    }

    .title-card {
        border-radius: 0 0 20px 20px;
    }

    .scrollable-container {
        max-height: calc(100vh - 450px);
        overflow-y: auto;
    }

    .content-page {
        overflow-y: clip!important;
    }
</style>