import { combineURLs } from "@/utils";
import { QuestionScore } from "models";

const baseURL = "https://localhost:7179/question-scores";

export const getQuestionScoresByTestScoreId = async (testScoreId: number) => {
  const url = new URL(combineURLs(baseURL, `/by-test-score-id/${testScoreId}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });
  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }

  return JSON.parse(text) as QuestionScore[];
};

export const getQuestionScoreById = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }

  return JSON.parse(text) as QuestionScore;
};

export const getQuestionScoreByUserAndQuestionId = async (
  userId: number,
  questionId: number
) => {
  const url = new URL(
    combineURLs(baseURL, `/by-user-and-question-id/${userId}&${questionId}`)
  );

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });
  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }

  return JSON.parse(text) as QuestionScore;
};

// export const deleteQuestionScore = async (id: number) => {
//   const url = new URL(combineURLs(baseURL, `/${id}`));

//   const response = await fetch(url, {
//     method: "DELETE",
//     credentials: "include",
//   });

//   return response.ok;
// };

// export const createQuestionScore = async (questionScore: QuestionScore) => {
//   const response = await fetch(baseURL, {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     credentials: "include",
//     body: JSON.stringify({ ...questionScore }),
//   });

//   return response.ok;
// };
