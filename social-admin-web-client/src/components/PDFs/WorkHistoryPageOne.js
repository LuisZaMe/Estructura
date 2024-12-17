import React from 'react';
import { Page, Text, View, Image } from "@react-pdf/renderer";
import { style } from "../../styles/PdfStyle";
import PdfFooter from './PdfFooter';
import PdfHeader from './PdfHeader';
import moment from 'moment';
import 'moment/locale/es';

const WorkHistoryPageOne = ({study, index}) => {
  const testImage = "/images/image-icon.png";

  const candidateStartDate = moment(study.candidateStartDate);
  const companyStartDate = moment(study.companyStartDate);
  const candidateEndDate = moment(study.candidateEndDate);
  const companyEndDate = moment(study.companyEndDate);

  candidateStartDate.locale('es');
  companyStartDate.locale('es');
  candidateEndDate.locale('es');
  companyEndDate.locale('es');

  const companyData = [
    {
      name: 'Razón Social',
      candidate: study.candidateBusinessName,
      company: study.companyBusinessName,
    },
    {
      name: 'Giro',
      candidate: study.candidateRole,
      company: study.companyRole,
    },
    {
      name: 'Teléfono',
      candidate: study.candidatePhone,
      company: study.companyPhone,
    },
    {
      name: 'Domicilio',
      candidate: study.candidateAddress,
      company: study.companyAddress,
    },
    {
      name: 'Fecha de ingreso',
      candidate: candidateStartDate.format("LL"),
      company: companyStartDate.format("LL")
    },
    {
      name: 'Fecha de egreso',
      candidate: candidateEndDate.format("LL"),
      company: companyEndDate.format("LL")
    },
    {
      name: 'Puesto inicial',
      candidate: study.candidateInitialRole,
      company: study.companyInitialRole,
    },
    {
      name: 'Puesto final',
      candidate: study.candidateFinalRole,
      company: study.companyFinalRole,
    },
    {
      name: 'Salario Inicial',
      candidate: study.candidateStartSalary,
      company: study.companyStartSalary,
    },
    {
      name: 'Salario Final',
      candidate: study.candidateEndSalary,
      company: study.companyEndSalary,
    },
    {
      name: 'Prestaciones',
      candidate: study.candidateBenefits,
      company: study.companyBenefits,
    },
    {
      name: 'Motivo de salida',
      candidate: study.candidateResignationReason,
      company: study.companyResignationReason
    },
    {
      name: 'Jefe inmediato & puesto',
      candidate: study.candidateDirectBoss,
      company: study.companyDirectBoss,
    },
    {
      name: 'Sindicalizado',
      candidate: study.candidateLaborUnion,
      company: study.companyLaborUnion,
    },
  ]
  return (
    <>
      <Page size={"A4"} style={{ width: '100%' }}>
        <View fixed>
          <PdfHeader />
        </View>
        <View style={style.generalData}>
          <View style={style.generalDataSection}>
            {index == 0 ? <Text style={[style.label, { color: '#07DDA5', marginVertical: 10, }]}>E. TRAYECTORIA LABORAL</Text> : null}
            <Text style={[style.label, { color: '#1bb9ef', marginVertical: 10, }]}>{index + 1}. {study.companyName}</Text>
            <Text style={[style.label, { marginVertical: 10, }]}>Información proporcionada por</Text>
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '32%' }]} />
              <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Candidato</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '32%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Empresa</Text>
              </View>
            </View>
            {companyData.map(data => {
              return (
                <View style={{ flexDirection: 'row' }}>
                  <View style={[style.boxBig, { width: '32%' }]}>
                    <Text style={[style.tableLabel, { padding: 5 }]}>{data.name}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '32%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{data.candidate}</Text>
                  </View>
                  <View style={[style.boxBig, { width: '32%' }]}>
                    <Text style={[style.tableText, { padding: 5 }]}>{data.company}</Text>
                  </View>
                </View>
              )
            })}
            <View style={{ flexDirection: 'row' }}>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>Recomendable</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.recommended}</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableLabel, { paddingVertical: 5 }]}>¿Por qué?</Text>
              </View>
              <View style={[style.boxBig, style.startCenter, { width: '24%' }]}>
                <Text style={[style.tableText, { paddingVertical: 5 }]}>{study.recommendedReasonWhy}</Text>
              </View>
            </View>

            <View style={{ width: '96%', flexDirection: 'row', marginTop: 20 }}>
              <View style={{ width: '15%' }}>
                <Text style={[style.tableText, { color: '#07DDA5', paddingRight: 5 }]}>
                  Observaciones:
                </Text>
              </View>
              <View style={{ width: '85%' }}>
                <Text style={[style.tableText]}>{study.observations}</Text>
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
            <View style={[style.flexRow, { width: '90%', padding: 15, marginVertical: 15 }]}>
              <View style={[style.center, { width: '50%', marginRight: 20 }]}>
                <Image src={testImage} />
              </View>
              <View style={[style.center, { width: '50%' }]}>
                <Image src={testImage} />
              </View>
            </View>
            <View style={[style.flexRow, { width: '90%', padding: 15, marginBottom: 15 }]}>
              <View style={[style.center, { width: '50%', marginRight: 20 }]}>
                <Image src={testImage} />
              </View>
              <View style={[style.center, { width: '50%' }]}>
                <Image src={testImage} />
              </View>
            </View>
            <View style={[style.flexRow, { width: '90%', padding: 15, marginBottom: 15 }]}>
              <View style={[style.center, { width: '50%', marginRight: 20 }]}>
                <Image src={testImage} />
              </View>
              <View style={[style.center, { width: '50%' }]}>
                <Image src={testImage} />
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
    </>
  )
}

export default WorkHistoryPageOne