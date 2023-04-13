<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-12 col-sm-12 col-12">
                    <div class="card mb-2 main-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h4>
                                        <i v-if="belongsToStaff === 'true'" class='user-status text-success bx bxs-user-check'></i>
                                        <i v-else class='user-status text-danger bx bxs-user-x'></i>
                                        {{ patientDetails.fullName }}
                                    </h4>
                                    <h4>
                                        <span v-if="belongsToStaff === 'true'" class="badge rounded-pill bg-success">Assigned</span>
                                        <span v-else="belongsToStaff" class="badge rounded-pill bg-danger">Only View</span>
                                    </h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div v-if="belongsToStaff === 'true'" class="col-xl-6 col-sm-12 col-md-12 col-12">
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
                                    <h5 class="me-4"><i class='bx bxs-capsule'></i> Current medication: {{ medicationCount }}</h5>
                                    <button class="btn btn-light btn-sm border confirm-btn me-4" @click="toggleAddMedication" :disabled="belongsToStaff !== 'true'">Add <i class='bx bx-plus add-icon'></i></button>
                                </div>
                                <div v-if="medicationCount !== 0" v-for="(medication, index) in patientMedications" :key="medication.Id" class="d-flex align-items-center mt-3">
                                    <span>{{ medication.MedicationName }}</span>
                                    <div class="progress w-25 ms-4 me-4 medication-bar">
                                        <div v-if="medication.Procentage < 100" class="progress-bar bg-warning progress-bar-striped progress-bar-animated" role="progressbar" :style="'width: ' + medication.Procentage + '%'" aria-valuenow="{{ medication.Procentage }}" aria-valuemin="0" aria-valuemax="100">
                                        </div>
                                        <div v-else class="progress-bar bg-success progress-bar-striped progress-bar-animated" role="progressbar" :style="'width: ' + medication.Procentage + '%'" aria-valuenow="{{ medication.Procentage }}" aria-valuemin="0" aria-valuemax="100">
                                        </div>
                                    </div>
                                    <span v-if="medication.Procentage < 100" class="ms-2">{{ medication.RemainingDays }}/{{ medication.DaysInMonth }} day <i class='bx bx-calendar-exclamation medication-progress-icon'></i></span>
                                    <span v-else class="ms-2">Exp. {{ medication.RemainingDays }} days ago <i class='bx bx-calendar-exclamation medication-progress-icon'></i></span>
                                </div>
                                <span v-else class="text-muted">No current medications found for this patient</span>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Add medication card -->
                <transition name="fade" mode="out-in">
                    <div class="col-xl-12 col-sm-12 col-12" v-if="showAddMedication" key="add-medication-card">
                        <div class="card mb-2 add-medication-card">
                            <div class="card-content">
                                <div class="card-body" v-if="!medicationAdded">
                                    <h5 class="mb-3"><i class='bx bxs-capsule'></i> <i class='bx bx-plus text-success'></i> Add new medication</h5>
                                    <div class="mb-3">
                                        <label for="medication-select" class="form-label">Medication</label>
                                        <select class="form-select border" id="medication-select" v-model="selectedMedication">
                                            <option disabled value="">Please select a medication</option>
                                            <option v-for="medication in medications" :key="medication.MedicationId" :value="medication.MedicationId">
                                                {{ medication.MedicationName }}
                                            </option>
                                        </select>
                                        <div v-if="medicationError" class="invalid-feedback">{{ medicationError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="start-date" class="form-label">Start Date</label>
                                        <input type="date" :class="{ 'is-invalid': startDateError }" class="form-control" id="start-date" v-model="startDate">
                                        <div v-if="startDateError" class="invalid-feedback">{{ startDateError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="end-date" class="form-label">End Date</label>
                                        <input type="date" :class="{ 'is-invalid': endDateError }" class="form-control" id="end-date" v-model="endDate">
                                        <div v-if="endDateError" class="invalid-feedback">{{ endDateError }}</div>
                                    </div>
                                    <div class="d-flex justify-content-between">
                                        <button class="btn btn-light btn-sm border confirm-btn" @click="cancelAddMedication">Cancel</button>
                                        <button class="btn btn-success btn-sm" @click="submitMedication">Submit</button>
                                    </div>
                                </div>
                                <div class="card-body d-flex justify-content-center align-items-center" v-else>
                                    <h5 class="text-center text-success">Medication added <i class='bx bxs-check-circle'></i></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </transition>
                <!-- Procedures card -->
                <div class="col-xl-6 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4"><i class='bx bx-injection'></i> Procedures: {{ procedureCount }}</h5>
                                    <button class="btn btn-light btn-sm border confirm-btn me-4" :disabled="belongsToStaff !== 'true'">Add <i class='bx bx-plus add-icon'></i></button>
                                </div>
                                <div v-if="procedureCount !== 0" v-for="(procedure, index) in patientProcedures" :key="procedure.Id" class="row d-flex align-items-center mt-3">
                                    <div class="col-4"><span>{{ procedure.ProcedureName }}</span></div>
                                    <div class="col-5">
                                        <span v-if="procedure.Status === 'Completed'" class="w-100 badge rounded-pill bg-success procedure-badge border">Completed on {{ formatDate(procedure.ActionDate) }} </span>
                                        <span v-else-if="procedure.Status === 'Scheduled'" class="w-100 badge rounded-pill bg-warning procedure-badge border">Scheduled on {{ formatDate(procedure.ActionDate) }} </span>
                                        <span v-else class="w-100 badge rounded-pill bg-danger procedure-badge border">Cancelled on {{ formatDate(procedure.ActionDate) }} </span>
                                    </div>
                                    <div class="col-3"><span class="procedure-link">View more</span></div>
                                </div>
                                <span v-else class="text-muted">No procedures found for this patient</span>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Examination card -->
                <div class="col-xl-6 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4"><i class='bx bx-clipboard'></i> Examination records: {{ examinationCount }}</h5>
                                </div>
                                <div v-if="examinationCount !== 0" v-for="(examination, index) in patientExaminations" :key="examination.Id" class="row d-flex align-items-center mt-3">
                                    <div class="col-4"><span>{{ formatExamDate(examination.ExaminationDate) }}</span></div>
                                    <div class="col-5"> <span class="w-100 badge rounded-pill bg-light examination-badge border">{{ examination.ExaminationType }}</span></div>
                                    <div class="col-3"><span class="procedure-link">View more</span></div>
                                </div>
                                <span v-else class="text-muted">No examinations found for this patient</span>
                                <div class="text-center mt-3">
                                    <button class="w-75 btn btn-light btn-sm rounded-pill border confirm-btn" :disabled="belongsToStaff !== 'true'">Add new examination record now <i class='bx bx-plus add-icon'></i></button>
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
    import moment from 'moment';
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
            const staffId = this.$store.state.user ? this.$store.state.user.staffId : null;
            return {
                staffId: staffId,
                procedures: [],
                patientDetails: [],
                showAddMedication: false,
                medications: [],
                selectedMedication: '',
                startDate: null,
                endDate: null,
                medicationError: null,
                startDateError: null,
                endDateError: null,
                medicationAdded: false,
                patientMedications: [],
                medicationCount: null,
                patientProcedures: [],
                procedureCount: null,
                patientExaminations: [],
                examinationCount: null,
            };
        },
        created() {
            this.selectedMedication = '';
            this.fetchProcedures();
            this.fetchPatientDetails();
            this.fetchMedications();
            this.fetchPatientMedicationList();
            this.fetchPatientProcedureList();
            this.fetchPatientExaminationList();
        },
        methods: {
            // Format date
            formatDate(date) {
                return moment(date).format("DD/MM/YYYY");
            },
            // Format examination date
            formatExamDate(date) {
                return moment(date).format("Do MMM YYYY");
            },
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
            toggleAddMedication() {
                this.showAddMedication = !this.showAddMedication;
            },
            async fetchMedications() {
                try {
                    const response = await axios.get("/api/medication/GetAllMedications");
                    if (response.data.success) {
                        console.log(response.data);
                        this.medications = response.data.medications;
                    } else {
                        console.log('Failed to get medication details:', response.data.message);
                    }
                } catch (error) {
                    console.error('Error fetching medication details:', error);
                }
            },
            cancelAddMedication() {
                this.showAddMedication = false;
            },
            // Add new medication record
            async submitMedication() {
                this.medicationError = null;
                this.startDateError = null;
                this.endDateError = null;

                if (!this.selectedMedication) {
                    this.medicationError = 'Please select a medication';
                }

                if (!this.startDate) {
                    this.startDateError = 'Please select a start date';
                }

                if (!this.endDate) {
                    this.endDateError = 'Please select an end date';
                }

                if (!this.medicationError && !this.startDateError && !this.endDateError) {
                    // Submit the form, save the data, etc.
                    try {
                        const response = await axios.post('/api/patient/AddPatientMedicationRecord', {
                            patientId: this.patientId,
                            medicationId: this.selectedMedication,
                            staffId: this.staffId,
                            startDate: this.startDate,
                            endDate: this.endDate,
                        });

                        if (response.data.success) {
                            this.fetchPatientMedicationList();
                            this.medicationAdded = true;
                            setTimeout(() => {
                                this.medicationAdded = false;
                                this.showAddMedication = false;
                                this.resetForm();
                            }, 2000);
                        } else {
                            console.log('Error adding medication record');
                        }
                    } catch (error) {
                        console.error(error);
                    }
                }
            },
            resetForm() {
                this.selectedMedication = null;
                this.startDate = null;
                this.endDate = null;
            },
            // Get medications list
            async fetchPatientMedicationList() {
                try {
                    const response = await axios.post('/api/patient/GetPatientMedicationList', { patientId: this.patientId });

                    if (response.data.success) {
                        this.patientMedications = response.data.patientMedications;
                        this.medicationCount = response.data.medicationCount;
                    } else {
                        console.log('Failed to get medications list', response.data.message);
                    }
                } catch (error) {
                    console.error(error);
                }
            },
            // Get procedures list
            async fetchPatientProcedureList() {
                try {
                    const response = await axios.post('/api/patient/GetPatientProcedureList', { patientId: this.patientId });
                    if (response.data.success) {
                        this.patientProcedures = response.data.patientProcedures;
                        this.procedureCount = response.data.procedureCount;
                    } else {
                        console.log('Failed to get procedure list', response.data.message);
                    }
                } catch (error) {
                    console.error(error);
                }
            },
            // Get examination list
            async fetchPatientExaminationList() {
                try {
                    const response = await axios.post('/api/patient/GetPatientExaminationList', { patientId: this.patientId });
                    if (response.data.success) {
                        console.log(response.data);
                        this.patientExaminations = response.data.patientExamination;
                        this.examinationCount = response.data.examinationCount;
                    } else {
                        console.log('Failed to get examination list', response.data.message);
                    }
                } catch (error) {
                    console.error(error);
                }
            },
        }
        
    }

</script>

<style lang="scss">
    @import "../sass/app.scss";

    .user-status {
        font-size: 28px;
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

    .examination-badge {
        font-size: 12px !important;
        color: $neutral-black!important;
    }

    .procedure-link {
        font-size: 12px;
        color: $main-blue;
        text-transform:uppercase;
        text-decoration:underline;
        cursor: pointer;
    }

    .fade-enter-active, .fade-leave-active {
        transition: opacity .5s !important;
    }

    .fade-enter, .fade-leave-to {
        opacity: 0 !important;
    }

    .add-medication-card {
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.4)!important;
        border: 1px solid $green!important;
    }
</style>
