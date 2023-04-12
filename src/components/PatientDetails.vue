<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-12 col-sm-12 col-12">
                    <div class="card mb-2 main-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <!--<h4>{{ patientDetails.fullName }}</h4>-->
                                    <h4><i class='user-status bx bxs-user-check'></i>Patient Name</h4>
                                    <h4><span class="badge rounded-pill bg-success">Assigned </span></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4">Upcoming appointment:</h5>
                                </div>
                                <h6><i class='calendar-icon bx bx-calendar-check'></i> 11:30 10/03/2023</h6>
                                <div class="row">
                                    <div class="col">
                                        <h6 class="fw-bold">Confirm?* <i class='app-lock bx bxs-lock-alt text-danger'></i></h6>
                                    </div>
                                    <div class="col d-flex">
                                        <button class="btn btn-light btn-sm border confirm-btn me-4"><i class='bx bx-check text-success'></i> Yes</button>
                                        <button class="btn btn-light btn-sm border confirm-btn"><i class='bx bx-x text-danger'></i> No</button>
                                    </div>
                                    <span class="app-note text-muted mt-2">*Available to confirm during/after appointment</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4"><i class='bx bxs-capsule'></i> Current medication: 2</h5>
                                    <button class="btn btn-light btn-sm border confirm-btn me-4">Add <i class='bx bx-plus add-icon'></i></button>
                                </div>
                                <div class="d-flex align-items-center mt-3">
                                    <span>Medication A</span>
                                    <div class="progress w-25 ms-4 me-4 medication-bar">
                                        <div class="progress-bar bg-success progress-bar-striped progress-bar-animated" role="progressbar" style="width:40%" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100">
                                        </div>
                                    </div>
                                    <span class="ms-2">7/30 day <i class='bx bx-calendar-exclamation medication-progress-icon'></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4"><i class='bx bx-injection'></i> Procedures: 2</h5>
                                    <button class="btn btn-light btn-sm border confirm-btn me-4">Add <i class='bx bx-plus add-icon'></i></button>
                                </div>
                                <div class="row d-flex align-items-center mt-3">
                                    <div class="col-4"><span>Medication A</span></div>
                                    <div class="col-5"> <span class="badge rounded-pill bg-success procedure-badge border">Completed on 15/02/23 </span></div>
                                    <div class="col-3"><span class="procedure-link">View more</span></div>
                                </div>
                                <div class="row d-flex align-items-center mt-3">
                                    <div class="col-4"><span>Medication A</span></div>
                                    <div class="col-5"> <span class="badge rounded-pill bg-warning procedure-badge border">Scheduled on 15/02/23 </span></div>
                                    <div class="col-3"><span class="procedure-link">View more</span></div>
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
    import axios from "axios";

    export default {
        name: "PatientDetails",
        props: {
            patientId: {
                type: Number,
                required: true,
            },
            belongsToStaff: {
                type: Boolean,
                required: true,
            },
        },
        data() {
            return {
                procedures: [],
                patientDetails: [],
            };
        },
        created() {
            this.fetchProcedures();
            this.fetchPatientDetails();
        },
        methods: {
            // Get patient`s procedures
            async fetchProcedures() {
                try {
                    console.log(this.patientId);
                    const response = await axios.post("/api/patient/GetPatientProceduresByPatientId", {
                            patientId: this.patientId,
                    });
                    if (response.data.success) {
                        console.log(response.data);
                        this.procedures = response.data.procedures;
                    } else {
                        console.log('Failed to get procedures');
                    }
                } catch (error) {
                    console.error('Error fetching procedures:', error);
                }
            },
            // Get patient`s details
            async fetchPatientDetails() {
                try {
                    const response = await axios.post("/api/patient/GetPatientDetailsByPatientId", {
                        patientId: this.patientId,
                    });
                    if (response.data.success) {
                        this.patientDetails = response.data;
                    } else {
                        console.log('Failed to get patient details:', response.data.message);

                    }
                } catch (error) {
                    console.error('Error fetching patient details:', error);
                }
            },
        }
        
    }

</script>

<style lang="scss">
    @import "../sass/app.scss";

    .user-status {
        font-size: 28px;
        color: $green;
        position: relative;
        top: 3px;
        padding-right: 10px;
    }

    .calendar-icon {
        font-size: 20px;
        color: $orange;
        padding-right: 5px;
    }

    .app-lock {
        font-size: 22px;
        position: relative;
        top: 3px;
    }

    .confirm-btn {
        padding-right: 10px;
        
        i {
            font-size: 19px;
            position: relative;
            top: 3px;
        }
    }

    .app-note {
        font-size: 14px;
    }

    h5 i {
        font-size: 24px;
        color: $main-blue;
        position: relative;
        top: 3px;
    }

    .add-icon {
        font-size: 22px;
        color: $main-blue;
    }

    .btn-add {
        background: red;
    }

    .medication-bar {
        height: 10px!important;
    }

    .medication-progress-icon {
        font-size: 20px;
        position: relative;
        top: 3px;
        color: $orange;
    }

    .procedure-badge {
        font-size: 12px!important;
    }

    .procedure-link {
        font-size: 12px;
        color: $main-blue;
        text-transform:uppercase;
        text-decoration:underline;
        cursor: pointer;
    }
</style>
