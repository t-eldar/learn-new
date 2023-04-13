import { combineURLs } from "@/utils";
import { CreateQuestionRequest, Question, UpdateQuestionRequest } from "models";

const baseURL = "https://localhost:7179/questions";

export const getQuestionsByTestId = async (testId: number) => {
  const url = new URL(combineURLs(baseURL, `/by-test-id/${testId}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();

  return JSON.parse(text) as Question[];
};

export const getQuestionById = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();

  return JSON.parse(text) as Question;
};

export const deleteQuestion = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));

  const response = await fetch(url, {
    method: "DELETE",
    credentials: "include",
  });

  return response.ok;
};

export const createQuestion = async (request: CreateQuestionRequest) => {
  const response = await fetch(baseURL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify({ ...request }),
  });

  return response.ok;
};

export const updateQuestion = async (request: UpdateQuestionRequest) => {
  const url = new URL(combineURLs(baseURL, `/${request.id}`));

  const response = await fetch(url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    credentials: "include",
    body: JSON.stringify({ ...request }),
  });

  return response.ok;
};
