class Company {
    constructor(companyName, companyPhone, legalAddress, paymentMethod, corporateName, taxId, taxRegime) {
        this.companyName = companyName
        this.companyPhone = companyPhone
        this.direccionFiscal = legalAddress
        this.payment = { id: paymentMethod }
        this.razonSocial = corporateName
        this.rfc = taxId
        this.regimenFiscal = taxRegime
    }
}

export default Company