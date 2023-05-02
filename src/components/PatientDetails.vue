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
                                        <span v-if="belongsToStaff === 'true' && user.isFullyAuth" class="badge rounded-pill bg-success">Assigned</span>
                                        <span v-else class="badge rounded-pill bg-danger">Only View</span>
                                    </h4>
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
                                <div v-if="appointmentStatus == 1">
                                    <h6><i class='calendar-icon bx bx-calendar-check'></i>{{ formatAppDate(appointmentData.AppointmentDate) }}</h6>
                                    <div class="row">
                                        <div class="col">
                                            <h6 class="fw-bold">Confirm?* <i class='app-lock bx bxs-lock-alt text-danger'></i></h6>
                                        </div>
                                        <div class="col d-flex">
                                            <button class="btn btn-light btn-sm border confirm-btn me-4" :disabled="belongsToStaff !== 'true' || user.isFullyAuth != true"><i class='bx bx-check text-success'></i> Yes</button>
                                            <button class="btn btn-light btn-sm border confirm-btn" :disabled="belongsToStaff !== 'true' || user.isFullyAuth != true"><i class='bx bx-x text-danger'></i> No</button>
                                        </div>
                                        <span class="app-note text-muted mt-2">*Available to confirm during/after appointment</span>
                                    </div>
                                </div>
                                <span v-else class="text-muted">No upcoming appointments found</span>
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
                                    <button class="btn btn-light btn-sm border confirm-btn me-4" @click="toggleAddMedication" :disabled="belongsToStaff !== 'true' || user.isFullyAuth != true">Add <i class='bx bx-plus add-icon'></i></button>
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
                                            <option disabled selected value="">Please select a medication</option>
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
                                    <button class="btn btn-light btn-sm border confirm-btn me-4" @click="toggleAddProcedure" :disabled="belongsToStaff !== 'true' || user.isFullyAuth != true">Add <i class='bx bx-plus add-icon'></i></button>
                                </div>
                                <div v-if="!currentProcedure">
                                    <div v-if="procedureCount !== 0" v-for="(procedure, index) in patientProcedures" :key="procedure.Id" class="row d-flex align-items-center mt-3">
                                        <div class="col-4"><span>{{ procedure.ProcedureName }}</span></div>
                                        <div class="col-5">
                                            <span v-if="procedure.Status === 'Completed'" class="w-100 badge rounded-pill bg-success procedure-badge border">Completed on {{ formatDate(procedure.ActionDate) }} </span>
                                            <span v-else-if="procedure.Status === 'Scheduled'" class="w-100 badge rounded-pill bg-warning procedure-badge border">Scheduled on {{ formatDate(procedure.ActionDate) }} </span>
                                            <span v-else class="w-100 badge rounded-pill bg-danger procedure-badge border">Cancelled on {{ formatDate(procedure.ActionDate) }} </span>
                                        </div>
                                        <div class="col-3"><span class="procedure-link" @click="currentProcedure = procedure">View more</span></div>
                                    </div>
                                    <span v-else class="text-muted">No procedures found for this patient</span>
                                </div>
                                <div v-else>
                                    <h5 class="mb-3">Procedure Details</h5>
                                    <p><strong>Procedure Name:</strong> {{ currentProcedure.ProcedureName }}</p>
                                    <p><strong>Procedure Date:</strong> {{ formatDate(currentProcedure.ActionDate) }}</p>
                                    <p><strong>Procedure Status:</strong> {{ currentProcedure.Status }}</p>
                                    <p><strong>Procedure Description:</strong></p>
                                    <p>{{ currentProcedure.Description }}</p>
                                    <button class="btn btn-light btn-sm border confirm-btn mt-2" @click="currentProcedure = null">Go back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Add procedure card -->
                <transition name="fade" mode="out-in">
                    <div class="col-xl-12 col-sm-12 col-12" v-if="showAddProcedure" key="add-procedure-card">
                        <div class="card mb-2 add-medication-card">
                            <div class="card-content">
                                <div class="card-body" v-if="!procedureAdded">
                                    <h5 class="mb-3"><i class='bx bx-injection'></i> <i class='bx bx-plus text-success'></i> Add new procedure</h5>
                                    <div class="mb-3">
                                        <label for="procedure-select" class="form-label">Procedures</label>
                                        <select class="form-select border" id="procedure-select" v-model="selectedProcedure" :class="{ 'is-invalid': procedureError }">
                                            <option disabled selected value="">Please select a procedure</option>
                                            <option v-for="procedure in procedures" :key="procedure.ProcedureId" :value="procedure.ProcedureId">
                                                {{ procedure.ProcedureName }}
                                            </option>
                                        </select>
                                        <div v-if="procedureError" class="invalid-feedback">{{ procedureError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="procedure-date" class="form-label">Procedure Date</label>
                                        <input type="date" :class="{ 'is-invalid': procedureDateError }" class="form-control" id="procedure-date" v-model="procedureDate">
                                        <div v-if="procedureDateError" class="invalid-feedback">{{ procedureDateError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="procedure-description" class="form-label">Procedure Description</label>
                                        <textarea :class="{ 'is-invalid': procedureDescriptionError }" class="form-control border" id="procedure-description" v-model="procedureDescription"></textarea>
                                        <div v-if="procedureDescriptionError" class="invalid-feedback">{{ procedureDescriptionError }}</div>
                                    </div>
                                    <div class="d-flex justify-content-between">
                                        <button class="btn btn-light btn-sm border confirm-btn" @click="cancelAddProcedure">Cancel</button>
                                        <button class="btn btn-success btn-sm" @click="submitProcedure">Submit</button>
                                    </div>
                                </div>
                                <div class="card-body d-flex justify-content-center align-items-center" v-else>
                                    <h5 class="text-center text-success">Procedure added <i class='bx bxs-check-circle'></i></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </transition>
                <!-- Examination card -->
                <div class="col-xl-6 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4"><i class='bx bx-clipboard'></i> Examination records: {{ examinationCount }}</h5>
                                </div>
                                <div v-if="!currentExamination">
                                    <div v-if="examinationCount !== 0" v-for="(examination, index) in patientExaminations" :key="examination.Id" class="row d-flex align-items-center mt-3">
                                        <div class="col-4"><span>{{ formatExamDate(examination.ExaminationDate) }}</span></div>
                                        <div class="col-5"> <span class="w-100 badge rounded-pill bg-light examination-badge border">{{ examination.ExaminationType }}</span></div>
                                        <div class="col-3"><span class="procedure-link" @click="currentExamination = examination">View more</span></div>
                                    </div>
                                    <span v-else class="text-muted">No examinations found for this patient</span>
                                    <div class="text-center mt-3">
                                        <button class="w-75 btn btn-light btn-sm rounded-pill border confirm-btn" @click="toggleAddExamination" :disabled="belongsToStaff !== 'true' || user.isFullyAuth != true">Add new examination record now <i class='bx bx-plus add-icon'></i></button>
                                    </div>
                                </div>
                                <div v-else>
                                    <h5 class="mb-3">Examination Details</h5>
                                    <p><strong>Examination Date:</strong> {{ formatExamDate(currentExamination.ExaminationDate) }}</p>
                                    <p><strong>Examination Type:</strong> {{ currentExamination.ExaminationType }}</p>
                                    <p><strong>Examination Analysis:</strong></p>
                                    <p>{{ currentExamination.Analysis }}</p>
                                    <button class="btn btn-light btn-sm border confirm-btn mt-2" @click="currentExamination = null">Go back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Add examination card -->
                <transition name="fade" mode="out-in">
                    <div class="col-xl-12 col-sm-12 col-12" v-if="showAddExamination" key="add-examination-card">
                        <div class="card mb-2 add-examination-card">
                            <div class="card-content">
                                <div class="card-body" v-if="!examinationAdded">
                                    <h5 class="mb-3"><i class='bx bx-clipboard'></i> <i class='bx bx-plus text-success'></i> Add new examination</h5>
                                    <div class="mb-3">
                                        <label for="examination-select" class="form-label">Examination types</label>
                                        <select class="form-select border" id="examination-select" v-model="selectedExamination" :class="{ 'is-invalid': examinationError }">
                                            <option disabled selected value="">Please select an examination</option>
                                            <option v-for="examination in examinations" :key="examination.ExaminationId" :value="examination.ExaminationId">
                                                {{ examination.ExaminationType }}
                                            </option>
                                        </select>
                                        <div v-if="examinationError" class="invalid-feedback">{{ examinationError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="examination-date" class="form-label">Examination Date</label>
                                        <input type="date" :class="{ 'is-invalid': examinationDateError }" class="form-control" id="examination-date" v-model="examinationDate">
                                        <div v-if="examinationDateError" class="invalid-feedback">{{ examinationDateError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="examination-description" class="form-label">Examination analysis</label>
                                        <textarea :class="{ 'is-invalid': examinationAnalysisError }" class="form-control border" id="examination-description" v-model="examinationAnalysis"></textarea>
                                        <div v-if="examinationAnalysisError" class="invalid-feedback">{{ examinationAnalysisError }}</div>
                                    </div>
                                    <div class="d-flex justify-content-between">
                                        <button class="btn btn-light btn-sm border confirm-btn" @click="cancelAddExamination">Cancel</button>
                                        <button class="btn btn-success btn-sm" @click="submitExamination">Submit</button>
                                    </div>
                                </div>
                                <div class="card-body d-flex justify-content-center align-items-center" v-else>
                                    <h5 class="text-center text-success">Examination added <i class='bx bxs-check-circle'></i></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </transition>
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
        computed: {
            user() {
                return this.$store.state.user;
            },
        },
        data() {
            const staffId = this.$store.state.user ? this.$store.state.user.staffId : null;
            return {
                staffId: staffId,
                procedures: [],
                patientDetails: [],
                showAddMedication: false,
                showAddProcedure: false,
                showAddExamination: false,
                medications: [],
                procedures: [],
                examinations: [],
                selectedMedication: '',
                selectedProcedure: '',
                selectedExamination: '',
                startDate: null,
                endDate: null,
                medicationError: null,
                startDateError: null,
                endDateError: null,
                procedureError: null,
                procedureDateError: null,
                procedureDescriptionError: null,
                examinationError: null,
                examinationDateError: null,
                examinationAnalysisError: null,
                medicationAdded: false,
                procedureAdded: false,
                examinationAdded: false,
                patientMedications: [],
                medicationCount: null,
                patientProcedures: [],
                procedureCount: null,
                patientExaminations: [],
                examinationCount: null,
                appointmentData: [],
                appointmentStatus: null,
                currentProcedure: null,
                currentExamination: null,
            };
        },
        created() {
            this.selectedMedication = '';
            this.selectedProcedure = '';
            this.selectedExamination = '',
            this.fetchProcedures();
            this.fetchPatientDetails();
            this.fetchMedications();
            this.fetchProcedureList();
            this.fetchExaminationList();
            this.fetchPatientMedicationList();
            this.fetchPatientProcedureList();
            this.fetchPatientExaminationList();
            this.fetchUpcomingAppointment();
        },
        methods: {
            // Format date
            formatDate(date) {
                return moment(date).format("DD/MM/YY");
            },
            // Format examination date
            formatExamDate(date) {
                return moment(date).format("Do MMM YYYY");
            },
            // Format appoinment date
            formatAppDate(date) {
                return moment(date).format("HH:mm Do MMM YYYY");
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
            toggleAddProcedure() {
                this.showAddProcedure = !this.showAddProcedure;
            },
            toggleAddExamination() {
                this.showAddExamination = !this.showAddExamination;
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
            cancelAddProcedure() {
                this.showAddProcedure = false;
            },
            cancelAddExamination() {
                this.showAddExamination = false;
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
            resetProcedureForm() {
                this.selectedProcedure = null;
                this.procedureDate = null;
                this.procedureDescription = null;
            },
            resetExaminationForm() {
                this.selectedExamination = null;
                this.examinationDate = null;
                this.examinationAnalysis = null;
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
                        this.patientExaminations = response.data.patientExamination;
                        this.examinationCount = response.data.examinationCount;
                    } else {
                        console.log('Failed to get examination list', response.data.message);
                    }
                } catch (error) {
                    console.error(error);
                }
            },
            // Get the most closest appoinment
            async fetchUpcomingAppointment() {
                try {
                    const response = await axios.post('/api/patient/GetUpcomingAppointmentByPatientId', {
                        patientId: this.patientId,
                        staffId: this.staffId,
                    });
                    if (response.data.success) {
                        console.log(response.data);
                        this.appointmentData = response.data.appointment;
                        this.appointmentStatus = response.data.appointmentStatus;
                    } else {
                        console.log('Failed to get appointment details', response.data.message);
                    }
                } catch (error) {
                    console.error(error);
                }
            },
            // Get procedure list for dropdown
            async fetchProcedureList() {
                try {
                    const response = await axios.get("/api/procedure/GetAllProcedures");
                    if (response.data.success) {
                        this.procedures = response.data.procedures;
                    } else {
                        console.log('Failed to get procedure list:', response.data.message);
                    }
                } catch (error) {
                    console.error('Error fetching procedures list:', error);
                }
            },
            // Add new procedure record
            async submitProcedure() {
                this.procedureError = null;
                this.procedureDateError = null;
                this.procedureDescriptionError = null;

                if (!this.selectedProcedure) {
                    this.procedureError = 'Please select a procedure';
                }

                if (!this.procedureDate) {
                    this.procedureDateError = 'Please select a procedure date';
                }

                if (!this.procedureDescription) {
                    this.procedureDescriptionError = 'Please write some description';
                }

                if (!this.procedureError && !this.procedureDateError && !this.procedureDescriptionError) {
                    try {
                        const response = await axios.post('/api/patient/AddPatientProcedureRecord', {
                            patientId: this.patientId,
                            procedureId: this.selectedProcedure,
                            procedureDate: this.procedureDate,
                            procedureDescription: this.procedureDescription,
                        });

                        if (response.data.success) {
                            this.fetchPatientProcedureList();
                            this.procedureAdded = true;
                            setTimeout(() => {
                                this.procedureAdded = false;
                                this.showAddProcedure = false;
                                this.resetProcedureForm();
                            }, 2000);
                        } else {
                            console.log('Error adding procedure record');
                        }
                    } catch (error) {
                        console.error(error);
                    }
                }
            },
            // Get examination list for dropdown
            async fetchExaminationList() {
                try {
                    const response = await axios.get("/api/examination/GetAllExaminations");
                    if (response.data.success) {
                        this.examinations = response.data.examinations;
                    } else {
                        console.log('Failed to get examinations list:', response.data.message);
                    }
                } catch (error) {
                    console.error('Error fetching examinations list:', error);
                }
            },
            // Add new examination record
            async submitExamination() {
                this.examinationError = null;
                this.examinationDateError = null;
                this.examinationAnalysisError = null;

                if (!this.selectedExamination) {
                    this.examinationError = 'Please select an examination type';
                }

                if (!this.examinationDate) {
                    this.examinationDateError = 'Please select an examination date';
                }

                if (!this.examinationAnalysis) {
                    this.examinationAnalysisError = 'Please write examination analysis';
                }

                if (!this.examinationError && !this.examinationDateError && !this.examinationAnalysisError) {
                    try {
                        const response = await axios.post('/api/patient/AddPatientExaminationRecord', {
                            patientId: this.patientId,
                            examinationId: this.selectedExamination,
                            examinationDate: this.examinationDate,
                            examinationAnalysis: this.examinationAnalysis,
                        });

                        if (response.data.success) {
                            this.fetchPatientExaminationList();
                            this.examinationAdded = true;
                            setTimeout(() => {
                                this.examinationAdded = false;
                                this.showAddExamination = false;
                                this.resetExaminationForm();
                            }, 2000);
                        } else {
                            console.log('Error adding examination record');
                        }
                    } catch (error) {
                        console.error(error);
                    }
                }
            },
        }
        
    }

</script>

<style lang="scss" scoped>
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
