<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-6 col-sm-6 col-md-6 col-6">
                    <div class="card mb-2 head-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="">Active Prescriptions</h5>
                                    <h2 class="text-success">{{ prescriptionSummary.ActivePrescriptions }}</h2>
                                    <span class="badge rounded-pill badge-show">Click to show <i class='bx bx-show'></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-6 col-md-12 col-6">
                    <div class="card mb-2 head-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="text-center">
                                    <h5 class="">Expired Prescriptions</h5>
                                    <h2 class="text-danger">{{ prescriptionSummary.ExpiredPrescriptions }}</h2>
                                    <span class="badge rounded-pill badge-show">Click to show <i class='bx bx-show'></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2 head-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="text-center">
                                    <span class="badge border w-75 rounded-pill badge-show badge-all">Show all prescriptions<i class='bx bx-show'></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div v-if="groupedPrescriptions !== 0" v-for="(prescription, index) in groupedPrescriptions" :key="prescription.MedicationId" class="col-xl-6 col-sm-12 col-md-6 col-12">
                    <div class="accordion mb-3" :id="`accordion-${prescription.MedicationId}`">
                        <div class="accordion-item">
                            <div class="accordion-header d-flex justify-content-between align-items-center" :class="{ 'accordion-border-blue': isAccordionOpen[index] }" @click="toggleAccordion(index, prescription.MedicationId)">
                                <div class="vertical-line"></div>
                                <div class="left-side text-center">
                                    <p>{{ prescription.MedicationName }}</p>
                                    <p>Last Prescribed:</p>
                                    <p>{{ formatPresDate(prescription.MostRecentPrescriptionDate)}}</p>
                                </div>
                                <div class="right-side ms-5 d-flex justify-content-between align-items-center">
                                    <div class="text-center">
                                        <p class="fw-bold">Active</p>
                                        <p class="count-status text-success">{{ prescription.ActivePrescriptionsCount }}</p>
                                    </div>
                                    <button class="btn btn-link" type="button" data-bs-toggle="collapse" :data-bs-target="`#accordion-collapse-${prescription.MedicationId}`" aria-expanded="false" :aria-controls="`accordion-collapse-${prescription.MedicationId}`" @click="toggleAccordion(index, prescription.MedicationId)">
                                        <i class='bx bx-chevron-down accordion-icon' :class="{ 'rotate-chevron': isAccordionOpen[index] }"></i>
                                    </button>
                                    <div class="text-center">
                                        <p class="fw-bold">Expiring</p>
                                        <p class="count-status text-danger">{{ prescription.ExpiringPrescriptionsCount }}</p>
                                    </div>
                                </div>
                            </div>
                            <div :id="`accordion-collapse-${prescription.MedicationId}`" class="accordion-collapse collapse" :data-bs-parent="`#accordion-${prescription.MedicationId}`">
                                <div class="accordion-body pt-2 p-0">
                                    <table class="table table-borderless">
                                        <thead>
                                            <tr>
                                                <th scope="col">Patient Name</th>
                                                <th scope="col" class="text-center">End Date</th>
                                                <th scope="col" class="text-center">Extend?</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td colspan="3">
                                                    <div class="table-scroll">
                                                        <table class="table table-borderless">
                                                            <tbody>
                                                                <tr>
                                                                    <td>Bob Johnson</td>
                                                                    <td class="text-center"><span class="badge rounded-pill bg-success">17/03/2023</span></td>
                                                                    <td><button class="btn btn-success btn-sm border text-white">Confirm</button></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Bob Johnson</td>
                                                                    <td class="text-center"><span class="badge rounded-pill bg-danger">17/03/2023</span></td>
                                                                    <td><button class="btn btn-success btn-sm border text-white">Confirm</button></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Bob Johnson</td>
                                                                    <td class="text-center"><span class="badge rounded-pill bg-success">17/03/2023</span></td>
                                                                    <td><button class="btn btn-success btn-sm border text-white">Confirm</button></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Bob Johnson</td>
                                                                    <td class="text-center"><span class="badge rounded-pill bg-danger">17/03/2023</span></td>
                                                                    <td><button class="btn btn-success btn-sm border text-white">Confirm</button></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Bob Johnson</td>
                                                                    <td class="text-center"><span class="badge rounded-pill bg-success">17/03/2023</span></td>
                                                                    <td><button class="btn btn-success btn-sm border text-white">Confirm</button></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Bob Johnson</td>
                                                                    <td class="text-center"><span class="badge rounded-pill bg-danger">17/03/2023</span></td>
                                                                    <td><button class="btn btn-success btn-sm border text-white">Confirm</button></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
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
    import Collapse from 'bootstrap/js/src/collapse';
    import axios from "axios";
    import moment from 'moment';

    export default {
        data() {
            const staffId = this.$store.state.user ? this.$store.state.user.staffId : null;
            return {
                staffId: staffId,
                isAccordionOpen: [],
                accordionCollapse: null,
                prescriptionSummary: [],
                groupedPrescriptions: [],
            };
        },
        created() {
            this.fetchPrescriptionSummary();
            this.fetchGroupedPrescription();
            this.fetchGroupedPrescription();
        },
        methods: {
            // Format prescription date
            formatPresDate(date) {
                return moment(date).format("DD MMM YYYY");
            },
            toggleAccordion(index, medicationId) {
                const updatedIsAccordionOpen = [...this.isAccordionOpen];
                updatedIsAccordionOpen[index] = !updatedIsAccordionOpen[index];
                this.isAccordionOpen = updatedIsAccordionOpen;

                const collapseElement = document.getElementById(`accordion-collapse-${medicationId}`);
                const accordionCollapse = new Collapse(collapseElement);

                if (this.isAccordionOpen[index]) {
                    accordionCollapse.show();
                } else {
                    accordionCollapse.hide();
                }
            },
            // Get prescription summary
            async fetchPrescriptionSummary() {
                try {
                    const response = await axios.post("/api/prescription/GetPrescriptionSummary", {
                        staffId: this.staffId,
                    });
                    if (response.data.success) {
                        this.prescriptionSummary = response.data.prescriptionSummary;
                    } else {
                        console.log('Failed to get prescription summary:', response.data.message);

                    }
                } catch (error) {
                    console.error('Error fetching prescription summary:', error);
                }
            },
            // Get prescription grouped by medication
            async fetchGroupedPrescription() {
                try {
                    const response = await axios.post("/api/prescription/GetStaffPrescriptionsGroupedByMedication", {
                        staffId: this.staffId,
                    });
                    if (response.data.success) {
                        this.groupedPrescriptions = response.data.groupedPrescriptions;
                    } else {
                        console.log('Failed to get grouped prescriptions:', response.data.message);

                    }
                } catch (error) {
                    console.error('Error fetching  grouped prescriptions:', error);
                }
            },
        },
    };

</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";

    h2 {
        font-size: 2.2rem;
    }

    .badge-show {
        background: $neutral-grey-pale;
        text-transform:uppercase;
        font-size: .8rem;
        color: $neutral-black;
        padding: 7px;
  

        i {
            font-size: 16px;
            position: relative;
            top: 2px;
            padding-left: 3px;
        }
    }

    .head-card {
        border-radius: 15px;
    }

    .head-card:hover {
        background: $neutral-grey-pale;
        cursor: pointer;
    }

    .badge-all {
        font-size: 1.2rem;
        text-transform: none;

        i {
            font-size: 18px;
            padding-left: 10px;
        }
    }

    /* Accordion styling */
    .accordion-item {
        position: relative;
    }

    .accordion-header {
        display: flex;
        align-items: stretch;
        padding: 1rem 1rem 0rem 1rem!important;
        background-color: #f8f9fa;
        border: 1px solid #dee2e6;
        border-radius: 0.25rem;
        cursor: pointer;
        height: 90px;
    }

    .left-side,
    .right-side {
        flex-basis: 50%;
    }

    .vertical-line {
        position: absolute;
        left: 50%;
        top: 0;
        bottom: 0;
        border-left: 2px dashed #dee2e6;
        z-index: 1;
        height: 90px;
    }

    .rotate {
        transform: rotate(180deg);
    }

    .accordion {
        border: 1px solid #dee2e6;
        border-radius: 0.25rem;
        box-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
        background-color: $neutral-white;
        overflow: hidden;
    }

    .accordion-header {
        padding: 1rem;
        background-color: #f8f9fa;
        border-bottom: 1px solid #dee2e6;
        font-weight: 500;
        color: #333;
    }

    .accordion-body {
        padding: 1rem;
        color: $neutral-black;
        box-shadow: 0 4px 6px rgba(1, 1, 1, 0.6) !important;
    }

    .accordion-icon {
        color: $main-blue-bright;
        font-size: 28px;
    }

    .accordion
    {
        p {
        line-height: .5rem;
        }
        p:nth-child(2) {
            font-size: .8rem;
            text-transform: uppercase;
            font-weight: bold;
        }
    }

    .count-status {
        font-size: 1rem!important;
    }

    .accordion-border-blue {
        border: 1px solid $main-blue-bright !important;
    }

    .table-scroll {
        max-height: 200px;
        overflow-y: auto;
        display: block;
    }

    .table {
        width: 100%;
        margin-bottom: 0;
        color: $neutral-black;
    }

    .table thead {
        display: table;
        width: 100%;
        table-layout: fixed;
    }

    .table tbody {
        display: table;
        width: 100%;
        table-layout: fixed;
    }

    .table th,
    .table td {
        padding: 0.7rem;
        vertical-align: top;
        border-bottom: 1px solid $neutral-grey-pale;
    }

    .table thead th {
        vertical-align: bottom;
        border: 2px solid #dee2e6;
        font-weight: bold;
        background-color: $neutral-grey-pale;
    }

    .table .btn {
        width: 100%;
    }

    .rotate-chevron {
        transform: rotate(180deg);
        transition: transform 0.3s ease;
    }



</style>