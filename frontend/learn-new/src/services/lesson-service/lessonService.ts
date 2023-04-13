import { combineURLs } from "@/utils";
import { CreateLessonRequest, Lesson, UpdateLessonRequest } from "models";

const baseURL = "https://localhost:7179/lessons";
export const getAllLessons = async () => {
  const response = await fetch(baseURL, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }

  return JSON.parse(text) as Lesson[];
};

export const getLessonById = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }

  return JSON.parse(text) as Lesson;
};

export const getLessonsByCourseId = async (courseId: number) => {
  const url = new URL(combineURLs(baseURL, `/by-course-id/${courseId}`));

  const response = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const text = await response.text();
  if (text.length === 0) {
    return undefined;
  }

  return JSON.parse(text) as Lesson[];
};

export const deleteLesson = async (id: number) => {
  const url = new URL(combineURLs(baseURL, `/${id}`));

  const response = await fetch(url, {
    method: "DELETE",
    credentials: "include",
  });

  return response.ok;
};

export const createLesson = async (request: CreateLessonRequest) => {
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

export const updateLesson = async (request: UpdateLessonRequest) => {
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
