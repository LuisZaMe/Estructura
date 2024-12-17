import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';

const EconomyPage = ({ study }) => {
  const services = [
    {
      name: "Luz",
      cost: study.studyEconomicSituation.electricity
    },
    {
      name: "Gas",
      cost: study.studyEconomicSituation.gas
    },
    {
      name: "Agua",
      cost: study.studyEconomicSituation.water
    },
    {
      name: "Predio",
      cost: study.studyEconomicSituation.propertyTax
    },
    {
      name: "Internet",
      cost: study.studyEconomicSituation.internet
    },
    {
      name: "Alimentación",
      cost: study.studyEconomicSituation.food
    },
    {
      name: "Gasolina",
      cost: study.studyEconomicSituation.gasoline
    },
    {
      name: "Vestido",
      cost: study.studyEconomicSituation.clothing
    },
    {
      name: "Gastos escolares",
      cost: study.studyEconomicSituation.schoolar
    },
    {
      name: "Infonavit",
      cost: study.studyEconomicSituation.infonavit
    },
    {
      name: "Créditos",
      cost: study.studyEconomicSituation.credits
    },
    {
      name: "Mantenimiento",
      cost: study.studyEconomicSituation.maintenance
    },
    {
      name: "Cable",
      cost: study.studyEconomicSituation.cable
    },
    {
      name: "Celulares",
      cost: study.studyEconomicSituation.cellphone
    },
    {
      name: "Diversiones",
      cost: study.studyEconomicSituation.entertainment
    },
    {
      name: "Otros",
      cost: study.studyEconomicSituation.miscellaneous
    },
    {
      name: "Renta",
      cost: study.studyEconomicSituation.rent
    },
  ];

  let total = 0;
  let incomingTotal = 0;
  let additionalIncomingTotal = 0;
  let creditTotal = 0;
  let estateTotal = 0;
  let vehicleTotal = 0;

  services.map(service => {
    total = total + service.cost;
  });

  return (
    <>
      <Page size={"A4"} style={{ width: '100%' }}>
        <View fixed>
          <PdfHeader />
        </View>
        <View style={style.generalData}>
          <View style={style.generalDataSection}>
            <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>C. ECONOMÍA</Text>
            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>SITUACIÓN ECONÓMICA</Text>
            <Text style={[style.label, { marginVertical: 10, }]}>EGRESOS</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '48%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Servicios</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '48%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Importe mensual</Text>
              </View>
            </View>

            {services.map(service => {
              return (
                <View style={{ flexDirection: 'row' }}>
                  <View style={[style.boxBig, { width: '48%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{service.name}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '48%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>${service.cost}</Text>
                  </View>
                </View>
              )
            })}

            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, { width: '48%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>TOTAL</Text>
              </View>
              <View style={[style.boxBig, { width: '48%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>${total}</Text>
              </View>
            </View>
          </View>
        </View>
        <View fixed style={[style.center, { position: "absolute", bottom: 0, width: '100%' }]}>
          <View style={[style.center, { width: '100%' }]}>
            <PdfFooter />
          </View>
        </View>
      </Page>

      <Page size={"A4"} style={{ width: '100%' }}>
        <View fixed>
          <PdfHeader />
        </View>
        <View style={style.generalData}>
          <View style={style.generalDataSection}>
            <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>C. ECONOMÍA</Text>
            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>INGRESOS</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '28%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Nombre</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '28%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Parentesco</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '40%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Monto (aportaciones)</Text>
              </View>
            </View>

            {
              study.studyEconomicSituation.incomingList.map(income => {
                incomingTotal = incomingTotal + income.amount;
                return (
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, { width: '28%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{income.name}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '28%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{income.relationship}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '40%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>${income.amount}</Text>
                    </View>
                  </View>
                )
              })
            }

            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, { width: '28%' }]} />
              <View style={[style.boxBig, { width: '28%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>TOTAL</Text>
              </View>
              <View style={[style.boxBig, { width: '40%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>${incomingTotal}</Text>
              </View>
            </View>

            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>INGRESOS ADICIONALES</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '28%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Actividad</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '28%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Periodo</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '40%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Ingresos</Text>
              </View>
            </View>

            {
              study.studyEconomicSituation.additionalIncomingList.map(income => {
                additionalIncomingTotal = additionalIncomingTotal + income.amount;
                return (
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, { width: '28%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{income.activity != "" ? income.activity : "Ninguno"}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '28%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{income.timeFrame}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '40%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>${income.amount}</Text>
                    </View>
                  </View>
                )
              })
            }

            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, { width: '28%' }]} />
              <View style={[style.boxBig, { width: '28%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>TOTAL</Text>
              </View>
              <View style={[style.boxBig, { width: '40%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>${additionalIncomingTotal}</Text>
              </View>
            </View>

            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>CRÉDITOS</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Banco/Institución</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>No. de cuenta</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Limite de crédito</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Adeudo</Text>
              </View>
            </View>

            {
              study.studyEconomicSituation.creditList.map(credit => {
                creditTotal = creditTotal + credit.debt;
                return (
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, { width: '24%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{credit.bank}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '24%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{credit.accountNumber}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '24%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{credit.creditLimit}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '24%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{credit.debt}</Text>
                    </View>
                  </View>
                )
              })
            }

            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, { width: '24%' }]} />
              <View style={[style.boxBig, { width: '24%' }]} />
              <View style={[style.boxBig, { width: '24%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>TOTAL</Text>
              </View>
              <View style={[style.boxBig, { width: '24%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>{creditTotal}</Text>
              </View>
            </View>

            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>BIENES</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Concepto</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Forma de adquisición</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Año de adquisición</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Titular</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Compra</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Actual</Text>
              </View>
            </View>

            {
              study.studyEconomicSituation.estateList.map(estate => {
                estateTotal = estateTotal + estate.currentValue;
                return (
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{estate.concept}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{estate.AcquisitionMethod}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{estate.AcquisitionDate}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{estate.owner}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{estate.purchaseValue}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{estate.currentValue}</Text>
                    </View>
                  </View>
                )
              })
            }

            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>TOTAL</Text>
              </View>
              <View style={[style.boxBig, { width: '16%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>{estateTotal}</Text>
              </View>
            </View>

            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>VEHÍCULOS</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Placas</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>No. de serie</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Marca & modelo</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Titular</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Compra</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '16%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Actual</Text>
              </View>
            </View>

            {
              study.studyEconomicSituation.vehicleList.map(vehicle => {
                vehicleTotal = vehicleTotal + vehicle.currentValue;
                return (
                  <View style={{ flexDirection: 'row' }}>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{vehicle.plates != "" ? vehicle.plates : "Ninguno"}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{vehicle.serialNumber}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{vehicle.brandAndModel}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>{vehicle.owner}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>${vehicle.purchaseValue}</Text>
                    </View>
                    <View style={[style.boxBig, { width: '16%' }]}>
                      <Text style={[style.tableText, { padding: 5 }]}>${vehicle.currentValue}</Text>
                    </View>
                  </View>
                )
              })
            }

            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]} />
              <View style={[style.boxBig, { width: '16%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>TOTAL</Text>
              </View>
              <View style={[style.boxBig, { width: '16%' }]}>
                <Text style={[style.tableText, { padding: 5 }]}>${vehicleTotal}</Text>
              </View>
            </View>

            <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
              <View style={{ width: '15%' }}>
                <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                  Análisis económico:
                </Text>
              </View>
              <View style={{ width: '85%' }}>
                <Text style={[style.tableText]}>{study.studyEconomicSituation.economicSituationSummary}</Text>
              </View>
            </View>
          </View>
        </View>
        <View fixed style={[style.center, { position: "absolute", bottom: 0, width: '100%', display: 'block' }]}>
          <View style={[style.center, { width: '100%' }]}>
            <PdfFooter />
          </View>
        </View>
      </Page>
    </>
  )
}

export default EconomyPage