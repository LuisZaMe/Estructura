import React from "react";
import { Document, Page, Text, View, Image } from "@react-pdf/renderer";
import GeneralDataPage from "../components/PDFs/GeneralDataPage";
import OficialDocumentationPage from "../components/PDFs/OficialDocumentationPage";
import DocumnetImages from "../components/PDFs/DocumnetImages";
import SocioeconomicStudyExtraData from "../components/PDFs/SocioeconomicStudyExtraData";
import EducationPage from "../components/PDFs/EducationPage";
import FamilyPage from "../components/PDFs/FamilyPage";
import EconomyPage from "../components/PDFs/EconomyPage";
import SocialPage from "../components/PDFs/SocialPage";
import WorkHistoryPageOne from "../components/PDFs/WorkHistoryPageOne";
import ImssValidationPage from "../components/PDFs/ImssValidationPage";
import PersonalReferencesPage from "../components/PDFs/PersonalReferencesPage";

const DocuPDF = ({ study }) => {
  if (study.workStudy == null) {
    return (
      <Document>
        <GeneralDataPage study={study} />
        <OficialDocumentationPage study={study} />
        <DocumnetImages study={study} />
        <SocioeconomicStudyExtraData study={study} />
        <EducationPage study={study} />
        <FamilyPage study={study} />
        <EconomyPage study={study} />
        <SocialPage study={study} />
        {
          study.studyLaboralTrayectoryList.map((company, index) => <WorkHistoryPageOne study={company} index={index} />)
        }
        <ImssValidationPage study={study} />
        {
          study.studyPersonalReferenceList.map((reference, index) => <PersonalReferencesPage study={reference} index={index} />)
        }
      </Document>
    );
  } else {
    return (
      <Document>
        {/* <GeneralDataPage study={study} /> */}
        <OficialDocumentationPage study={study} />
        <DocumnetImages study={study} />
        {/* <SocioeconomicStudyExtraData study={study} /> */}
        <EducationPage study={study} />
        {/* <FamilyPage study={study} /> */}
        {/* <EconomyPage study={study} /> */}
        {/* <SocialPage study={study} /> */}
        {
          study.studyLaboralTrayectoryList.map((company, index) => <WorkHistoryPageOne study={company} index={index} />)
        }
        <ImssValidationPage study={study} />
        {/* {
          study.studyPersonalReferenceList.map((reference, index) => <PersonalReferencesPage study={reference} index={index} />)
        } */}
      </Document>
    );
  }
};

export default DocuPDF;