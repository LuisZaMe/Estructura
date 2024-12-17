class Service {
    constructor(candidate, fields, interviewer, analyst, serviceType, document) {
        this.candidate = candidate
        this.fieldsToFill = {
            generalInformation: fields.overallData ? fields.overallData.selected : false,
            identificationCardPics: fields.ine ? fields.ine.selected : false,
            IMSSValidation: fields.imssValidation ? fields.imssValidation.selected : false,
            economicSituation: fields.employmentStatus ? fields.employmentStatus.selected : false,
            extracurricular: fields.extracurricular ? fields.extracurricular.selected : false,
            educationalLevel: fields.scholarship ? fields.scholarship.selected : false,
            family: fields.family ? fields.family.selected : false,
            personalReferences: fields.personalReferences ? fields.personalReferences.selected : false,
            pictures: fields.photos ? fields.photos.selected : false,
            recommendationLetter: fields.recommendationLetter ? fields.recommendationLetter.selected : false,
            resume: fields.summary ? fields.summary.selected : false,
            scholarVerification: fields.academicValidation ? fields.academicValidation.selected : false,
            social: fields.social ? fields.social.selected : false,
            workHistory: fields.workHistory ? fields.workHistory.selected : false
        }
        this.interviewer = interviewer
        this.analyst = analyst
        this.serviceType = serviceType
        this.workStudy = serviceType === 2 ? {
            AddressProof: document.proofOfAddress.selected,
            BirthCertificate: document.birthCertificate.selected,
            CriminalRecordLetter: document.criminalRecordCertificate.selected,
            CURP: document.curp.selected,
            IdentificationCard: document.ine.selected,
            MilitaryLetter: document.militaryServiceCertificate.selected,
            RFC: document.taxId.selected,
            SocialSecurityNumber: document.imssNumber.selected,
            StudiesProof: document.proofOfStudy.selected
        } : null
        this.socioeconomicStudy = serviceType === 1 ? {
            StudiesProof: document.proofOfStudy.selected,
            SocialSecurityNumber: document.imssNumber.selected,
            IdentificationCard: document.ine.selected,
            CURP: document.curp.selected,
            AddressProof: document.proofOfAddress.selected,
            BirthCertificate: document.birthCertificate.selected
        } : null
    }
}

export default Service